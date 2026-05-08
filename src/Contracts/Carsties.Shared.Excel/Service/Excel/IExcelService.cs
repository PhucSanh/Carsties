using System;
using Carsties.Shared.Excel.DTOs;

namespace Carsties.Shared.Excel.Service.Excel;

public interface IExcelService
{
    byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName);

    ImportError<T> ImportFromExcel<T>(Stream fileStream, Func<T, string?>? validateRule = null) where T : new();
    byte[] GenerateTemplate<T>();

}
