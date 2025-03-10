using System;
using Volo.Abp.Application.Dtos;

namespace Yal.BookStore.Authors
{
    /// <summary>
    /// 用于获取作者列表的DTO
    /// </summary>
    [Serializable]
    public class AuthorGetListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 编码过滤条件
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 名字过滤条件
        /// </summary>
        public string? Name { get; set; }
    }
}