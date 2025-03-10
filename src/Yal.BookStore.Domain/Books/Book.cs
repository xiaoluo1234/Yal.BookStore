using System;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace Yal.BookStore.Books
{
    /// <summary>
    /// 图书
    /// </summary>
    [CacheName(nameof(Book))]
    public class Book : Entity<Guid>
    {
        #region Prop

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; private set; }

        /// <summary>
        /// 作者编码
        /// </summary>
        public string? AuthorCode { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected Book()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">图书ID</param>
        /// <param name="title">标题</param>
        /// <param name="authorCode">作者编码</param>
        /// <param name="description">描述</param>
        public Book(
            Guid id,
            string title,
            string authorCode,
            string? description = null
        ) : base(id)
        {
            SetTitle(title)
                .SetAuthorCode(authorCode)
                .SetDescription(description);
        }

        #endregion

        #region Method

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>图书实例</returns>
        public Book SetTitle(string title)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: BookConstants.MaxLength.Title);
            return this;
        }

        /// <summary>
        /// 设置作者编码
        /// </summary>
        /// <param name="authorCode">作者编码</param>
        /// <returns>图书实例</returns>
        public Book SetAuthorCode(string authorCode)
        {
            AuthorCode = Check.NotNullOrWhiteSpace(authorCode, nameof(authorCode), maxLength: BookConstants.MaxLength.AuthorCode);
            return this;
        }

        /// <summary>
        /// 设置描述
        /// </summary>
        /// <param name="description">描述</param>
        /// <returns>图书实例</returns>
        public Book SetDescription(string? description)
        {
            Description = Check.Length(description, nameof(description), maxLength: BookConstants.MaxLength.Description);
            return this;
        }

        #endregion
    }
}