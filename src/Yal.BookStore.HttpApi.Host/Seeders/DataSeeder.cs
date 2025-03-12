using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Yal.BookStore.Authors;
using Yal.BookStore.Books;

namespace Yal.BookStore.HttiApi.Host.Seeders
{
    public class DataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly DataReader _dataReader;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public DataSeeder(
            DataReader dataReader,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository
            )
        {
            _dataReader = dataReader;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // 图书播种
            await CreateBooksAsync();

            // 作者播种
            await CreateAuthorsAsync();

        }

        /// <summary>
        /// 抽离播种方法
        /// </summary>
        /// <typeparam name="TSeed"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="reSeed"></param>
        /// <param name="repository"></param>
        /// <param name="getSeedDataAsync"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task SeedDataAsync<TSeed, TEntity>(
            bool reSeed,
            IRepository<TEntity> repository,
            Func<Task<IEnumerable<TSeed>>> getSeedDataAsync)
            where TEntity : class, IEntity
        {
            if (await repository.CountAsync() > 0)
            {
                if (reSeed)
                {
                    await repository.DeleteAsync(x => true);
                }
                else
                {
                    return;
                }
            }

            var items = (await getSeedDataAsync()).ToList();
            if (items.Count == 0)
            {
                return;
            }

            List<TEntity> entities = new List<TEntity>();

            try
            {
                var constructorInfo = typeof(TEntity).GetConstructors().FirstOrDefault() ?? throw new InvalidOperationException($"未找到构造函数，无法实例化 {typeof(TEntity).Name} 类型");

                var parameterTypes = constructorInfo.GetParameters();

                Dictionary<string, PropertyInfo> propertyCache = new Dictionary<string, PropertyInfo>();

                foreach (var param in parameterTypes)
                {
                    var property = typeof(TSeed).GetProperty(param.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property != null)
                    {
                        propertyCache[param.Name] = property;
                    }
                }
                var dbName = repository.GetDbSetAsync().Result.EntityType;

                foreach (var item in items)
                {
                    object[] parameters = new object[parameterTypes.Length];

                    for (int i = 0; i < parameterTypes.Length; i++)
                    {
                        var parameterInfo = parameterTypes[i];
                        var parameterName = parameterInfo.Name;

                        if (!propertyCache.TryGetValue(parameterName, out var property))
                        {
                            continue;
                        }

                        var value = property.GetValue(item);
                        var targetType = parameterInfo.ParameterType;

                        if (Nullable.GetUnderlyingType(targetType) != null)
                        {
                            if (value == null)
                            {
                                parameters[i] = null;
                            }
                            else
                            {
                                parameters[i] = Convert.ChangeType(value, Nullable.GetUnderlyingType(targetType));
                            }
                        }
                        else
                        {
                            parameters[i] = Convert.ChangeType(value, targetType);
                        }
                    }

                    var entity = constructorInfo.Invoke(parameters);

                    entities.Add((TEntity)entity);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("种子数据播种异常，请检查数据源是否符合构造函数的校验条件", ex);
            }

            await repository.InsertManyAsync(entities);
        }


        /// <summary>
        /// 图书播种
        /// </summary>
        /// <param name="reSeed"></param>
        /// <returns></returns>
        private async Task CreateBooksAsync(bool reSeed = false)
        {
            await SeedDataAsync(
                reSeed: reSeed,
                repository: _bookRepository,
                getSeedDataAsync: _dataReader.GetBooksSeedDataAsync
            );
        }

        /// <summary>
        /// 作者播种
        /// </summary>
        /// <param name="reSeed"></param>
        /// <returns></returns>
        private async Task CreateAuthorsAsync(bool reSeed = false)
        {
            await SeedDataAsync(
                reSeed: reSeed,
                repository: _authorRepository,
                getSeedDataAsync: _dataReader.GetAuthorsSeedDataAsync
            );
        }
    }
}
