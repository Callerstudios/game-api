namespace GameApi.Models;

public class PlayerQueryParameters
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public string? Username { get; set; }

    public string SortBy { get; set; } = "username";

    public bool Descending { get; set; }
}