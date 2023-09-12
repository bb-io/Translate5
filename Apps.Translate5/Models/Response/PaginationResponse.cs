namespace Apps.Translate5.Models.Response;

public class PaginationResponse<T> : ResponseWrapper<T[]>
{
    public int Total { get; set; }
}