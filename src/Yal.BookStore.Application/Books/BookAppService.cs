using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yal.BookStore.Authors;

namespace Yal.BookStore.Books
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, BookGetListDto, BookCreateDto, BookUpdateDto>, IBookAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorAppService _authorAppService;

        public BookAppService(
            IBookRepository bookRepository,
            IAuthorAppService authorAppService
            ) : base(bookRepository)
        {
            _bookRepository = bookRepository;
            _authorAppService = authorAppService;
        }

        #region ==书籍相关==

        /// <summary>
        /// 添加一个书籍
        /// </summary>
        /// <param name="input">书籍创建输入</param>
        /// <returns>添加的书籍DTO</returns>
        public override async Task<BookDto> CreateAsync(BookCreateDto input)
        {
            // 检查创建权限
            await CheckCreatePolicyAsync();

            var book = new Book(
                GuidGenerator.Create(),
                input.Title,
                input.AuthorCode,
                input.Description
            );

            await _bookRepository.InsertAsync(book);

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        /// <summary>
        /// 更新一个书籍
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <param name="input">书籍更新输入</param>
        /// <returns>更新后的书籍DTO</returns>
        public override async Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {
            // 检查更新权限
            await CheckUpdatePolicyAsync();

            var book = await _bookRepository.GetAsync(id);

            book.SetTitle(input.Title);
            book.SetAuthorCode(input.AuthorCode);
            book.SetDescription(input.Description);

            await _bookRepository.UpdateAsync(book);

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        /// <summary>
        /// 删除一个书籍
        /// </summary>
        /// <param name="id">书籍ID</param>
        public override async Task DeleteAsync(Guid id)
        {
            // 检查删除权限
            await CheckDeletePolicyAsync();

            await _bookRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除书籍
        /// </summary>
        /// <param name="input">批量删除信息</param>
        public async Task DeleteManyAsync(List<Guid> input)
        {
            // 检查删除权限
            await CheckDeletePolicyAsync();

            await _bookRepository.DeleteManyAsync(input);
        }

        /// <summary>
        /// 获取书籍列表
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>分页结果的书籍DTO列表</returns>
        public override async Task<PagedResultDto<BookDto>> GetListAsync(BookGetListDto input)
        {
            var query = await CreateFilteredQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);

            if (totalCount == 0) return new PagedResultDto<BookDto>(0, []);

            query = query.PageBy(input.SkipCount, input.MaxResultCount);
            var books = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<BookDto>(
                totalCount,
                ObjectMapper.Map<List<Book>, List<BookDto>>(books)
            );
        }

        /// <summary>
        /// 创建查询过滤条件
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns>查询语句</returns>
        protected override async Task<IQueryable<Book>> CreateFilteredQueryAsync(BookGetListDto input)
        {
            var query = await _bookRepository.GetQueryableAsync();

            query = query
                .WhereIf(!string.IsNullOrWhiteSpace(input.Title), x => x.Title.Contains(input.Title))
                .WhereIf(!string.IsNullOrWhiteSpace(input.AuthorCode), x => x.AuthorCode.Contains(input.AuthorCode));

            return query;
        }

        /// <summary>
        /// 获取单个书籍详情
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <returns>书籍DTO</returns>
        public override async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        #endregion
    }
}