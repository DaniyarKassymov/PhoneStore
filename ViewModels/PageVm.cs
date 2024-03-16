namespace Lesson.ViewModels;

public class PageVm
{
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }

    public PageVm(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}