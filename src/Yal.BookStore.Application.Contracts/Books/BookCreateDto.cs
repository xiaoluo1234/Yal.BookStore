using System;

namespace Yal.BookStore.Books
{
    /// <summary>
    /// 用于创建图书的DTO
    /// </summary>
    [Serializable]
    public class BookCreateDto
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