using System;


namespace Carsties.Shared.Excel.Attributes.Excel;

public class ExcelColumnAttribute : Attribute
{
    public string ColumnName { get; }
    public int Order { get; set; }

    public ExcelColumnAttribute(string columnName, int order = Int32.MaxValue)
    {
        ColumnName = columnName;
        Order = order;
    }

}
