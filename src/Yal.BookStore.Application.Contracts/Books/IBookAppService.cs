using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Yal.BookStore.Books
{
    public interface IBookAppService : ICrudAppService<BookDto, Guid, BookGetListDto, BookCreateDto, BookUpdateDto>
    {
        /// <summary>
        /// 添加一个书籍
        /// </summary>
        /// <param name="input">书籍创建输入</param>
        /// <returns>添加的书籍DTO</returns>
        Task<BookDto> CreateAsync(BookCreateDto input);

        /// <summary>
        /// 更新一个书籍
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <param name="input">书籍更新输入</param>
        /// <returns>更新后的书籍DTO</returns>
        Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input);

        /// <summary>
        /// 删除一个书籍
        /// </summary>
        /// <param name="id">书籍ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 批量删除书籍
        /// </summary>
        /// <param name="input">批量删除信息</param>
        Task DeleteManyAsync(List<Guid> input);

        /// <summary>
        /// 获取书籍列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>分页结果的书籍DTO列表</returns>
        Task<PagedResultDto<BookDto>> GetListAsync(BookGetListDto input);

        /// <summary>
        /// 获取单个书籍详情
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <returns>书籍DTO</returns>
        Task<BookDto> GetAsync(Guid id);
    }
}