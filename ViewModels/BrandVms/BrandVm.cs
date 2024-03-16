namespace Lesson.ViewModels.BrandVms;

public class BrandVm
{
    public long Id { get; set; }
    public required string? Name { get; init; }
    public required string Link { get; init; }
}