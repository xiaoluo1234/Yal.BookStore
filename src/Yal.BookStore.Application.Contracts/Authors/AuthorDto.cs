using System;
using Volo.Abp.Application.Dtos;

namespace Yal.BookStore.Authors
{
    /// <summary>
    /// 作者信息
    /// </summary>
    [Serializable]
    public class AuthorDto : EntityDto<Guid>
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