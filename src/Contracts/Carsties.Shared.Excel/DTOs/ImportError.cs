using System;

namespace Carsties.Shared.Excel.DTOs;

public class ImportError<T>
{
    public List<T> Data { get; set; } = new();
    public List<string> Errors { get; set; } = new();
    public bool IsSuccess => Errors.Count == 0;
}
