using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Yal.BookStore.Books;

namespace Yal.BookStore.Controllers.Books
{
    /// <summary>
    /// 书籍控制器
    /// </summary>
    [Area("bookstore")]
    [RemoteService(Name = "BookStore")]
    [Route("api/bookstore/books")]
    public partial class BookController(IBookAppService bookAppService)
    {
        /// <summary>
        /// 添加一个书籍
        /// </summary>
        /// <param name="input">书籍创建输入</param>
        /// <returns>添加的书籍DTO</returns>
        [HttpPost]
        public virtual async Task<BookDto> CreateAsync([FromBody] BookCreateDto input)
        {
            return await bookAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除一个书籍
        /// </summary>
        /// <param name="id">书籍ID</param>
        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] Guid id)
        {
            await bookAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除书籍
        /// </summary>
        /// <param name="input">批量删除信息</param>
        [HttpDelete]
        public async Task BulkDeleteAsync([FromBody] List<Guid> input)
        {
            await bookAppService.DeleteManyAsync(input);
        }

        /// <summary>
        /// 更新一个书籍
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <param name="input">书籍更新输入</param>
        /// <returns>更新后的书籍DTO</returns>
        [HttpPut("{id}")]
        public virtual async Task<BookDto> UpdateAsync(Guid id, [FromBody] BookUpdateDto input)
        {
            return await bookAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 获取书籍列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>分页结果的书籍DTO列表</returns>
        [HttpGet]
        public virtual async Task<PagedResultDto<BookDto>> GetListAsync([FromQuery] BookGetListDto input)
        {
            return await bookAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个书籍详情
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <returns>书籍DTO</returns>
        [HttpGet("{id}")]
        public virtual async Task<BookDto> GetAsync(Guid id)
        {
            return await bookAppService.GetAsync(id);
        }
    }
}