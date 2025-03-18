using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Yal.BookStore.DataStore;
public interface IDataCacheStore<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
{
    #region 查缓存

    /// <summary>
    /// 根据id查缓存
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(TKey id);

    /// <summary>
    /// 根据Code查缓存
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<TEntity> GetByCodeAsync(string code);

    /// <summary>
    /// 根据id批量拿缓存
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetManyAsync(IEnumerable<TKey> ids);

    /// <summary>
    /// 根据Code批量拿缓存
    /// </summary>
    /// <param name="codes"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetManyByCodesAsync(IEnumerable<string> codes);

    #endregion

    #region 写缓存

    /// <summary>
    /// 根据id写缓存
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task SetAsync(TKey id, TEntity entity);

    /// <summary>
    /// 根据Code批量写缓存
    /// </summary>
    /// <param name="code"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task SetByCodeAsync(string code, TEntity entity);

    /// <summary>
    /// 根据Code批量写缓存
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task SetManyByCodesAsync(Dictionary<string, TEntity> entities);

    /// <summary>
    /// 根据id批量写缓存
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task SetManyAsync(Dictionary<TKey, TEntity> entities);

    #endregion

    #region 删缓存
    /// <summary>
    /// 根据id删除缓存
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(TKey id);

    /// <summary>
    /// 根据Code删除缓存
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task RemoveByCodeAsync(string code);

    #endregion
}
