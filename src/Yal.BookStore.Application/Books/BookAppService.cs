﻿using System;
using System.Collections.Generic;
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
        private readonly IDataCacheStore<Author, Guid> _authorCacheStore;
        private readonly IDataCacheStore<Book, Guid> _bookCacheStore;

        public BookAppService(
            IBookRepository bookRepository,
            IDataCacheStore<Author, Guid> authorCacheStore,
            IDataCacheStore<Book, Guid> bookCacheStore
        ) : base(bookRepository)
        {
            _bookRepository = bookRepository;
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
            var query = await CreateFilteredQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);

            if (totalCount == 0) return new PagedResultDto<BookDto>(0, []);

            query = query.PageBy(input.SkipCount, input.MaxResultCount);
            var books = await AsyncExecuter.ToListAsync(query);

            var bookDtos = ObjectMapper.Map<List<Book>, List<BookDto>>(books);

            // 获取所有 AuthorCode
            var authorCodes = books.Select(x => x.AuthorCode).Distinct().ToList();

            // 通过 AuthorCode 查询缓存
            var authors = await _authorCacheStore.GetManyByCodesAsync(authorCodes!);

            // 构建 AuthorCode 到 AuthorName 的映射
            var authorDict = authors.ToDictionary(a => a.Code, a => a.Name);

            // 赋值 AuthorName
            foreach (var bookDto in bookDtos)
            {
                if (authorDict.TryGetValue(bookDto.AuthorCode, out var authorName))
                {
                    bookDto.AuthorName = authorName!;
                }
            }

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

            // 查找 Author 名称（优先缓存）
            var author = await _authorCacheStore.GetByCodeAsync(book.AuthorCode!);
            bookDto.AuthorName = author.Name!;

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