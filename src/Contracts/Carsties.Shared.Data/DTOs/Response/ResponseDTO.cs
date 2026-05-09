using System;

namespace Carsties.Shared.Data.DTOs.Response;

public class ResponseDTO<T>
{
    public T? Data { get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }

}
