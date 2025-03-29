using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yal.BookStore.Authors;
using Yal.BookStore.DataStore;

namespace Yal.BookStore.Books
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, BookGetListDto, BookCreateDto, BookUpdateDto>, IBookAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IDataCacheStore<Author, Guid> _authorCacheStore;
        private readonly IDataCacheStore<Book, Guid> _bookCacheStore;

        public BookAppService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IDataCacheStore<Author, Guid> authorCacheStore,
            IDataCacheStore<Book, Guid> bookCacheStore

        ) : base(bookRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorCacheStore = authorCacheStore;
            _bookCacheStore = bookCacheStore;
        }

        #region ==书籍相关==

        /// <summary>
        /// 添加一个书籍
        /// </summary>
        public override async Task<BookDto> CreateAsync(BookCreateDto input)
        {
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
        public override async Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {
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
        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();

            await _bookRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除书籍
        /// </summary>
        public async Task DeleteManyAsync(List<Guid> input)
        {
            await CheckDeletePolicyAsync();

            await _bookRepository.DeleteManyAsync(input);
        }

        /// <summary>
        /// 获取书籍列表，并返回作者名称
        /// </summary>
        public override async Task<PagedResultDto<BookDto>> GetListAsync(BookGetListDto input)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var query = await CreateFilteredQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);

            if (totalCount == 0) return new PagedResultDto<BookDto>(0, []);

            query = query.PageBy(input.SkipCount, input.MaxResultCount);
            var books = await AsyncExecuter.ToListAsync(query);

            var bookDtos = ObjectMapper.Map<List<Book>, List<BookDto>>(books);

            // 查找 Author 名称逻辑，不同分支采用不同的查询方式
            stopwatch.Stop();
            Console.WriteLine($"查询书籍列表耗时：{stopwatch.ElapsedMilliseconds}ms");
            return new PagedResultDto<BookDto>(totalCount, bookDtos);
        }

        /// <summary>
        /// 获取单个书籍详情，并返回作者名称
        /// </summary>
        public override async Task<BookDto> GetAsync(Guid id)
        {
            // 先查缓存
            var book = await _bookCacheStore.GetAsync(id);
            if (book == null)
            {
                book = await _bookRepository.GetAsync(id);
                await _bookCacheStore.SetAsync(id, book);
            }

            var bookDto = ObjectMapper.Map<Book, BookDto>(book);

            // 查找 Author 名称逻辑，不同分支采用不同的查询方式

            return bookDto;
        }
        #endregion

        protected override async Task<IQueryable<Book>> CreateFilteredQueryAsync(BookGetListDto input)
        {
            var query = await _bookRepository.GetQueryableAsync();

            query = query
                .WhereIf(!string.IsNullOrWhiteSpace(input.Title), x => x.Title.Contains(input.Title))
                .WhereIf(!string.IsNullOrWhiteSpace(input.AuthorCode), x => x.AuthorCode.Contains(input.AuthorCode));

            return query;
        }

    }
}