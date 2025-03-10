using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Yal.BookStore.Authors
{
    public interface IAuthorAppService : ICrudAppService<AuthorDto, Guid, AuthorGetListDto, AuthorCreateDto, AuthorUpdateDto>
    {
        /// <summary>
        /// 添加一个作者
        /// </summary>
        /// <param name="input">作者创建输入</param>
        /// <returns>添加的作者DTO</returns>
        Task<AuthorDto> CreateAsync(AuthorCreateDto input);

        /// <summary>
        /// 更新一个作者
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <param name="input">作者更新输入</param>
        /// <returns>更新后的作者DTO</returns>
        Task<AuthorDto> UpdateAsync(Guid id, AuthorUpdateDto input);

        /// <summary>
        /// 删除一个作者
        /// </summary>
        /// <param name="id">作者ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 批量删除作者
        /// </summary>
        /// <param name="input">批量删除信息</param>
        Task DeleteManyAsync(List<Guid> input);

        /// <summary>
        /// 获取作者列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>分页结果的作者DTO列表</returns>
        Task<PagedResultDto<AuthorDto>> GetListAsync(AuthorGetListDto input);

        /// <summary>
        /// 获取单个作者详情
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <returns>作者DTO</returns>
        Task<AuthorDto> GetAsync(Guid id);
    }
}