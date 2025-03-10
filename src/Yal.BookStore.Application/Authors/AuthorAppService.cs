using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Yal.BookStore.Authors
{
    public class AuthorAppService : CrudAppService<Author, AuthorDto, Guid, AuthorGetListDto, AuthorCreateDto, AuthorUpdateDto>, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorAppService(
            IAuthorRepository repository
            )
            : base(repository)
        {
            _authorRepository = repository;
        }

        #region ==作者相关==

        /// <summary>
        /// 添加一个作者
        /// </summary>
        /// <param name="input">作者创建输入</param>
        /// <returns>添加的作者DTO</returns>
        public override async Task<AuthorDto> CreateAsync(AuthorCreateDto input)
        {
            // 检查创建权限
            await CheckCreatePolicyAsync();

            var author = new Author(
                GuidGenerator.Create(),
                input.Code,
                input.Name,
                input.Description
            );

            var createdAuthor = await _authorRepository.InsertAsync(author);
            return ObjectMapper.Map<Author, AuthorDto>(createdAuthor);
        }

        /// <summary>
        /// 更新一个作者
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <param name="input">作者更新输入</param>
        /// <returns>更新后的作者DTO</returns>
        public override async Task<AuthorDto> UpdateAsync(Guid id, AuthorUpdateDto input)
        {
            // 检查更新权限
            await CheckUpdatePolicyAsync();

            var author = await _authorRepository.GetAsync(id);

            author.SetCode(input.Code);
            author.SetName(input.Name);
            author.SetDescription(input.Description);

            var updatedAuthor = await _authorRepository.UpdateAsync(author);
            return ObjectMapper.Map<Author, AuthorDto>(updatedAuthor);
        }

        /// <summary>
        /// 删除一个作者
        /// </summary>
        /// <param name="id">作者ID</param>
        public override async Task DeleteAsync(Guid id)
        {
            // 检查删除权限
            await CheckDeletePolicyAsync();

            await _authorRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除作者
        /// </summary>
        /// <param name="input">批量删除信息</param>
        public async Task DeleteManyAsync(List<Guid> input)
        {
            // 检查删除权限
            await CheckDeletePolicyAsync();

            await _authorRepository.DeleteManyAsync(input);
        }

        /// <summary>
        /// 获取作者列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>分页结果的作者DTO列表</returns>
        public override async Task<PagedResultDto<AuthorDto>> GetListAsync(AuthorGetListDto input)
        {
            var query = await CreateFilteredQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);

            if (totalCount == 0) return new PagedResultDto<AuthorDto>(0, new List<AuthorDto>());

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var authors = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        /// <summary>
        /// 创建查询过滤条件
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>查询语句</returns>
        protected override async Task<IQueryable<Author>> CreateFilteredQueryAsync(AuthorGetListDto input)
        {
            var query = await _authorRepository.GetQueryableAsync();

            query = query
                .WhereIf(!string.IsNullOrWhiteSpace(input.Code), x => x.Code.Contains(input.Code))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Name), x => x.Name.Contains(input.Name));

            return query;
        }

        /// <summary>
        /// 获取单个作者详情
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <returns>作者DTO</returns>
        public override async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            return ObjectMapper.Map<Author, AuthorDto>(author);
        }
        #endregion
    }
}