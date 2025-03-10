using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Yal.BookStore.Authors
{
    internal static class AuthorDbContextModelConfig
    {
        /// <summary>
        /// 【作者】模型配置
        /// </summary>
        public static void ConfigureAuthor(this ModelBuilder builder)
        {
            builder.Entity<Author>(b =>
            {
                b.ToTable(nameof(Author), table => table.HasComment("作者"));
                b.ConfigureByConvention();

                // 属性
                b.Property(x => x.Code).IsRequired().HasMaxLength(AuthorConstants.MaxLength.Code).HasComment("编码");
                b.Property(x => x.Name).IsRequired().HasMaxLength(AuthorConstants.MaxLength.Name).HasComment("名字");
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(AuthorConstants.MaxLength.Description).HasComment("描述");

                // 索引
                b.HasIndex(x => x.Code);
            });
        }
    }
}