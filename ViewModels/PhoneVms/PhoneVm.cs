namespace Lesson.ViewModels.PhoneVms;

public class PhoneVm
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required string? BrandName { get; init; }
    public required decimal Price { get; init; }
}