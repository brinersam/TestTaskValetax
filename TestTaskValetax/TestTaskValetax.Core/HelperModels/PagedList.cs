namespace TestTaskValetax.Core.HelperModels;
public class PagedList<T>
{
    public IReadOnlyList<T>? Data { get; init; } = Array.Empty<T>();

    public int TotalCount { get; init; }

    public int PageSize { get; init; }

    public int PageIndex { get; init; }

    public bool HasNextPage => PageSize * PageIndex < TotalCount;

    public bool HasPreviousPage => PageIndex > 1;
}