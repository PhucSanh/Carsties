using System;

namespace Carsties.Shared.Data.DTOs.Request;

public class RequestDTO
{
    public string? SearchQuery { get; set; }
    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 10;

    public string? SortBy { get; set; }
    public string? SortDirection { get; set; } = "asc";


}
