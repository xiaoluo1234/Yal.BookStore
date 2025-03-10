using System;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace Yal.BookStore.Authors
{
    /// <summary>
    /// 作者
    /// </summary>
    [CacheName(nameof(Author))]
    public class Author : Entity<Guid>
    {
        #region Prop

        /// <summary>
        /// 编码
        /// </summary>
        public string? Code { get; private set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected Author()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">作者ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        public Author(
            Guid id,
            string code,
            string name,
            string? description = null
        ) : base(id)
        {
            SetCode(code)
                .SetName(name)
                .SetDescription(description);
        }

        #endregion

        #region Method

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns>作者实例</returns>
        public Author SetCode(string code)
        {
            Code = Check.NotNullOrWhiteSpace(code, nameof(code), maxLength: AuthorConstants.MaxLength.Code);
            return this;
        }

        /// <summary>
        /// 设置名字
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns>作者实例</returns>
        public Author SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: AuthorConstants.MaxLength.Name);
            return this;
        }

        /// <summary>
        /// 设置描述
        /// </summary>
        /// <param name="description">描述</param>
        /// <returns>作者实例</returns>
        public Author SetDescription(string? description)
        {
            Description = Check.Length(description, nameof(description), maxLength: AuthorConstants.MaxLength.Description);
            return this;
        }

        #endregion
    }
}