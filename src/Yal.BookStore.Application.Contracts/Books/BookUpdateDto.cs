using System;

namespace Yal.BookStore.Books
{
    /// <summary>
    /// 用于更新图书信息的DTO
    /// </summary>
    [Serializable]
    public class BookUpdateDto
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