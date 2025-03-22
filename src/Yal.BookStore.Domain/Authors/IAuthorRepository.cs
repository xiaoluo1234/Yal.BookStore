using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Yal.BookStore.Authors;
public interface IAuthorRepository : IRepository<Author, Guid>
{
    /// <summary>
    /// 获取作者名字和编码的字典
    /// </summary>
    /// <returns></returns>
    Task<Dictionary<string, string>> GetAuthorCodeNameDic(List<string> codes);
}