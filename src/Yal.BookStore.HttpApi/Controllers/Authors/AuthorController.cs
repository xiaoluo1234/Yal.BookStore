using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Yal.BookStore.Authors;

namespace Yal.BookStore.Controllers.Authors
{
    /// <summary>
    /// 作者控制器
    /// </summary>
    [Area("bookstore")]
    [RemoteService(Name = "BookStore")]
    [Route("api/bookstore/authors")]
    public partial class AuthorController(IAuthorAppService authorAppService)
    {
        /// <summary>
        /// 添加一个作者
        /// </summary>
        /// <param name="input">作者创建输入</param>
        /// <returns>添加的作者DTO</returns>
        [HttpPost]
        public virtual async Task<AuthorDto> CreateAsync([FromBody] AuthorCreateDto input)
        {
            return await authorAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除一个作者
        /// </summary>
        /// <param name="id">作者ID</param>
        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] Guid id)
        {
            await authorAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除作者
        /// </summary>
        /// <param name="input">批量删除信息</param>
        [HttpDelete]
        public async Task BulkDeleteAsync([FromBody] List<Guid> input)
        {
            await authorAppService.DeleteManyAsync(input);
        }

        /// <summary>
        /// 更新一个作者
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <param name="input">作者更新输入</param>
        /// <returns>更新后的作者DTO</returns>
        [HttpPut("{id}")]
        public virtual async Task<AuthorDto> UpdateAsync(Guid id, [FromBody] AuthorUpdateDto input)
        {
            return await authorAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 获取作者列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>分页结果的作者DTO列表</returns>
        [HttpGet]
        public virtual async Task<PagedResultDto<AuthorDto>> GetListAsync([FromQuery] AuthorGetListDto input)
        {
            return await authorAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个作者详情
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <returns>作者DTO</returns>
        [HttpGet("{id}")]
        public virtual async Task<AuthorDto> GetAsync(Guid id)
        {
            return await authorAppService.GetAsync(id);
        }
    }
}