using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Yal.BookStore.Authors;
using Yal.BookStore.Books;

namespace Yal.BookStore.HttiApi.Host.Seeders
{
    public class DataReader : ITransientDependency
    {
        private static readonly string ExcelRootPath = Path.Combine(AppContext.BaseDirectory, "DataSource");
        public async Task<IEnumerable<BooksEdto>> GetBooksSeedDataAsync()
        {
            string sheetName = "Books";
            return await MiniExcel.QueryAsync<BooksEdto>(Path.Combine(ExcelRootPath, "Books.xlsx"), sheetName);

        }

        public async Task<IEnumerable<AuthorEdto>> GetAuthorsSeedDataAsync()
        {
            string sheetName = "Authors";
            return await MiniExcel.QueryAsync<AuthorEdto>(Path.Combine(ExcelRootPath, "Authors.xlsx"), sheetName);

        }
    }
}