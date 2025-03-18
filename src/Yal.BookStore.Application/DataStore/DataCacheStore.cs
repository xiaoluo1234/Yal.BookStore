using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using System;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Yal.BookStore.DataStore;
public class DataCacheStore<TEntity, TKey> : IDataCacheStore<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
{
    private readonly IServiceProvider _serviceProvider;

    public DataCacheStore(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private string GetIdCacheKey(TKey id) => $"id:{id}";
    private string GetCodeCacheKey(string code) => $"code:{code}";

    #region 查缓存

    /// <summary>
    /// 通过 Id 获取缓存或数据库中的实体
    /// </summary>
    public async Task<TEntity> GetAsync(TKey id)
    {
        var _repository = _serviceProvider.GetRequiredService<IRepository<TEntity, TKey>>();
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var cacheKey = GetIdCacheKey(id);
        var cachedEntity = await _cache.GetAsync(cacheKey);
        if (cachedEntity != null)
        {
            return cachedEntity;
        }

        var entity = await _repository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (entity != null)
        {
            await _cache.SetAsync(cacheKey, entity, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
        }

        return entity!;
    }

    /// <summary>
    /// 通过 Code 获取缓存或数据库中的实体
    /// </summary>
    public async Task<TEntity> GetByCodeAsync(string code)
    {
        var _repository = _serviceProvider.GetRequiredService<IRepository<TEntity, TKey>>();
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var cacheKey = GetCodeCacheKey(code);
        var cachedEntity = await _cache.GetAsync(cacheKey);
        if (cachedEntity != null)
        {
            return cachedEntity;
        }

        var entity = await _repository.FirstOrDefaultAsync(x => EF.Property<string>(x, "Code") == code);
        if (entity != null)
        {
            await _cache.SetAsync(cacheKey, entity, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });

            // 也存储 Id 缓存
            var idKey = GetIdCacheKey(entity.Id);
            await _cache.SetAsync(idKey, entity, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
        }

        return entity!;
    }

    /// <summary>
    /// 批量获取多个实体（Id）
    /// </summary>
    public async Task<List<TEntity>> GetManyAsync(IEnumerable<TKey> ids)
    {
        var _repository = _serviceProvider.GetRequiredService<IRepository<TEntity, TKey>>();
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var cacheKeys = ids.Select(id => GetIdCacheKey(id)).ToList();
        var cacheResults = await _cache.GetManyAsync(cacheKeys);

        var cacheEntities = cacheResults.Where(kv => kv.Value != null)
                                        .ToDictionary(kv => kv.Key, kv => kv.Value);

        var missingIds = ids.Where(id => !cacheEntities.ContainsKey(GetIdCacheKey(id))).ToList();

        if (missingIds.Count != 0)
        {
            var missingEntities = await _repository.GetListAsync(e => missingIds.Contains(e.Id));
            foreach (var entity in missingEntities)
            {
                var idKey = GetIdCacheKey(entity.Id);
                await _cache.SetAsync(idKey, entity, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
                });
                cacheEntities[idKey] = entity;
            }
        }

        return cacheKeys.Select(id => cacheEntities.TryGetValue(id, out var entity) ? entity : null)
                  .Where(e => e != null)
                  .ToList()!;
    }

    /// <summary>
    /// 根据实体Code字段批量拿缓存
    /// </summary>
    /// <param name="codes"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> GetManyByCodesAsync(IEnumerable<string> codes)
    {
        var _repository = _serviceProvider.GetRequiredService<IRepository<TEntity, TKey>>();
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var cacheKeys = codes.Select(code => GetCodeCacheKey(code)).ToList();
        var cacheResults = await _cache.GetManyAsync(cacheKeys);

        var cacheEntities = cacheResults
            .Where(kv => kv.Value != null)
            .ToDictionary(kv => kv.Key, kv => kv.Value);

        var missingCodes = codes.Where(code => !cacheEntities.ContainsKey(GetCodeCacheKey(code))).ToList();

        if (missingCodes.Count != 0)
        {
            var queryable = await _repository.GetQueryableAsync();
            var missingEntities = await queryable
                .Where(e => missingCodes.Contains(EF.Property<string>(e, "Code")))
                .ToListAsync();


            foreach (var entity in missingEntities)
            {
                var codeKey = GetCodeCacheKey(entity.GetType().GetProperty("Code")?.GetValue(entity) as string);

                var idKey = GetIdCacheKey(entity.Id);

                await _cache.SetAsync(codeKey, entity, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
                });

                await _cache.SetAsync(idKey, entity, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
                });

                cacheEntities[codeKey] = entity;
            }
        }

        return cacheKeys
            .Select(code => cacheEntities.TryGetValue(code, out var entity) ? entity : null)
            .Where(e => e != null)
            .ToList()!;
    }

    #endregion

    #region 写缓存

    /// <summary>
    /// 设置 Code 相关的缓存，同时存储 Id 相关缓存
    /// </summary>
    public async Task SetByCodeAsync(string code, TEntity entity)
    {
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var codeKey = GetCodeCacheKey(code);
        var idKey = GetIdCacheKey(entity.Id);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        };

        await _cache.SetAsync(codeKey, entity, options);
        await _cache.SetAsync(idKey, entity, options);
    }

    /// <summary>
    /// 通过 Id 存储实体到缓存
    /// </summary>
    public async Task SetAsync(TKey id, TEntity entity)
    {
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();
        await _cache.SetAsync(GetIdCacheKey(id), entity, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        });
    }

    /// <summary>
    /// 批量存储实体到缓存
    /// </summary>
    public async Task SetManyAsync(Dictionary<TKey, TEntity> entities)
    {
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        };

        foreach (var kvp in entities)
        {
            await _cache.SetAsync(GetIdCacheKey(kvp.Key), kvp.Value, options);
        }
    }

    /// <summary>
    /// 根据实体Code字段批量写缓存
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task SetManyByCodesAsync(Dictionary<string, TEntity> entities)
    {
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        };

        foreach (var kvp in entities)
        {
            var codeKey = GetCodeCacheKey(kvp.Key);
            var idKey = GetIdCacheKey(kvp.Value.Id);

            await _cache.SetAsync(codeKey, kvp.Value, options);
            await _cache.SetAsync(idKey, kvp.Value, options);
        }
    }

    #endregion

    #region 删缓存

    /// <summary>
    /// 通过 Id 删除缓存
    /// </summary>
    public async Task RemoveAsync(TKey id)
    {
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();
        await _cache.RemoveAsync(GetIdCacheKey(id));
    }

    /// <summary>
    /// 删除 Code 相关的缓存，同时删除 Id 缓存
    /// </summary>
    public async Task RemoveByCodeAsync(string code)
    {
        var _repository = _serviceProvider.GetRequiredService<IRepository<TEntity, TKey>>();
        var _cache = _serviceProvider.GetRequiredService<IDistributedCache<TEntity>>();

        var codeKey = GetCodeCacheKey(code);
        var cachedEntity = await _cache.GetAsync(codeKey);

        if (cachedEntity != null)
        {
            var idKey = GetIdCacheKey(cachedEntity.Id);
            await _cache.RemoveAsync(idKey);
        }

        await _cache.RemoveAsync(codeKey);
    }

    #endregion
}
