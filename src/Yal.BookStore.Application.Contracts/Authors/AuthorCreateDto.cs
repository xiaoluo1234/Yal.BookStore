using System;

namespace Yal.BookStore.Authors
{
    /// <summary>
    /// 用于创建作者的DTO
    /// </summary>
    [Serializable]
    public class AuthorCreateDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}