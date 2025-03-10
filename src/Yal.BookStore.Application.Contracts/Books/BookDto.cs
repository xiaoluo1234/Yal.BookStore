using System;
using Volo.Abp.Application.Dtos;

namespace Yal.BookStore.Books
{
    /// <summary>
    /// 图书信息
    /// </summary>
    [Serializable]
    public class BookDto : EntityDto<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者编码
        /// </summary>
        public string AuthorCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}