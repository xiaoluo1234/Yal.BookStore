using System;
using Volo.Abp.Application.Dtos;

namespace Yal.BookStore.Books
{
    /// <summary>
    /// 用于获取图书列表的DTO
    /// </summary>
    [Serializable]
    public class BookGetListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 标题过滤条件
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 作者编码过滤条件
        /// </summary>
        public string? AuthorCode { get; set; }
    }
}