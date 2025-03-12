using MiniExcelLibs;
using System.Collections.Generic;
using System.Linq;

namespace Arim.Ims.Data.Seeders;

/// <summary>
/// 提供从 Excel 文件中读取数据的辅助方法。
/// </summary>
/// <remarks>
/// 构造函数，初始化数据源文件路径。
/// </remarks>
/// <param name="dataSourceFile">数据源文件的路径。</param>
public class ExcelDataHelper(string dataSourceFile)
{
    private readonly string _dataSourceFile = dataSourceFile;

    /// <summary>
    /// 获取数据源信息。
    /// </summary>
    /// <typeparam name="T">数据源的类型。</typeparam>
    /// <param name="sheetName">Excel 页名称。</param>
    /// <returns>数据源列表。</returns>
    public IList<T> GetDataSourceList<T>(string sheetName) where T : class, new()
    {
        // 使用 MiniExcel 查询 Excel 文件，并将结果转换为泛型类型 T 的列表。
        var queryResult = MiniExcel.Query<T>(_dataSourceFile, sheetName).ToList();
        return queryResult;
    }
}
