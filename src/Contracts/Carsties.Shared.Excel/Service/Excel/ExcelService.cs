using System;
using System.Reflection;
using Carsties.Shared.Excel.Attributes.Excel;
using Carsties.Shared.Excel.DTOs;
using OfficeOpenXml;

namespace Carsties.Shared.Excel.Service.Excel;

public class ExcelService : IExcelService
{
    public byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add(sheetName);

        var propsWithAttr = typeof(T).GetProperties()
            .Select(p => new
            {
                Property = p,
                Attribute = p.GetCustomAttribute<ExcelColumnAttribute>()
            })
            .Where(x => x.Attribute != null)
            .OrderBy(x => x.Attribute!.Order)
            .ToList();

        for (int i = 0; i < propsWithAttr.Count; i++)
        {
            worksheet.Cells[1, i + 1].Value = propsWithAttr[i].Attribute!.ColumnName;

            var headerCell = worksheet.Cells[1, i + 1];
            headerCell.Style.Font.Bold = true;
            headerCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            headerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        var items = data.ToList();
        for (int rowIndex = 0; rowIndex < items.Count; rowIndex++)
        {
            var item = items[rowIndex];
            for (int colIndex = 0; colIndex < propsWithAttr.Count; colIndex++)
            {
                var info = propsWithAttr[colIndex];
                var rawValue = info.Property.GetValue(item);
                object? finalValue = null;

                if (rawValue != null)
                {
                    finalValue = rawValue;
                }

                worksheet.Cells[rowIndex + 2, colIndex + 1].Value = finalValue?.ToString();
            }
        }

        worksheet.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }

    public byte[] GenerateTemplate<T>()
    {
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Template");

        var props = typeof(T).GetProperties()
            .Select(p => p.GetCustomAttribute<ExcelColumnAttribute>())
            .Where(a => a != null)
            .OrderBy(a => a!.Order)
            .ToList();

        for (int i = 0; i < props.Count; i++)
        {
            var cell = worksheet.Cells[1, i + 1];
            cell.Value = props[i]!.ColumnName;
            cell.Style.Font.Bold = true;
        }

        worksheet.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }


    public ImportError<T> ImportFromExcel<T>(FileStream fileStream, Func<T, string?>? validateRule = null) where T : new()
    {
        var result = new ImportError<T>();
        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension?.Rows ?? 0;
        var colCount = worksheet.Dimension?.Columns ?? 0;
        if (rowCount < 2) return result;
        var props = typeof(T).GetProperties()
            .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<ExcelColumnAttribute>() })
            .Where(x => x.Attribute != null)
            .ToList();
        var headerMapping = new Dictionary<int, PropertyInfo>();
        for (int col = 1; col <= colCount; col++)
        {
            var headerName = worksheet.Cells[1, col].Text.Trim().ToLower();
            var match = props.FirstOrDefault(x => x.Attribute!.ColumnName.ToLower() == headerName);
            if (match != null)
            {
                headerMapping.Add(col, match.Property);
            }
        }
        for (int row = 2; row <= rowCount; row++)
        {
            var obj = new T();
            bool hasValue = false;
            foreach (var entry in headerMapping)
            {
                var cell = worksheet.Cells[row, entry.Key];
                if (cell.Value != null)
                {
                    var prop = entry.Value;
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    try
                    {
                        var convertedValue = Convert.ChangeType(cell.Value, targetType);
                        prop.SetValue(obj, convertedValue);
                        hasValue = true;
                    }
                    catch (Exception ex)
                    {
                        var colName = headerMapping[entry.Key].Name;
                        result.Errors.Add($"Row {row}: Failed to convert '{cell.Value}' to {targetType.Name} for column '{colName}'. Error: {ex.Message}");
                    }
                }
            }

            if (hasValue)
            {
                if (validateRule != null)
                {
                    var validationError = validateRule(obj);
                    if (validationError != null)
                    {
                        result.Errors.Add($"Row {row}: {validationError}");
                        continue;
                    }
                }
                result.Data.Add(obj);
            }
        }

        return result;
    }


}
