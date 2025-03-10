using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Yal.BookStore.Books
{
    internal static class BookDbContextModelConfig
    {
        /// <summary>
        /// 【图书】模型配置
        /// </summary>
        public static void ConfigureBook(this ModelBuilder builder)
        {
            builder.Entity<Book>(b =>
            {
                b.ToTable(nameof(Book), table => table.HasComment("图书"));
                b.ConfigureByConvention();

                // 属性
                b.Property(x => x.Title).IsRequired().HasMaxLength(BookConstants.MaxLength.Title).HasComment("书名");
                b.Property(x => x.AuthorCode).IsRequired().HasMaxLength(BookConstants.MaxLength.AuthorCode).HasComment("作者编码");
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(BookConstants.MaxLength.Description).HasComment("描述");

                // 索引
                b.HasIndex(x => x.AuthorCode);
            });
        }
    }
}