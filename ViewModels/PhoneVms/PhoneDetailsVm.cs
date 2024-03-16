using Lesson.Options;

namespace Lesson.ViewModels.PhoneVms;

public class PhoneDetailsVm
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string? Brand { get; set; }
    public required decimal Price { get; set; }
    public string? DisplayType { get; set; }
    public short? Ram { get; set; }
    public short? BatteryCapacity { get; set; }
    public string? Description { get; set; }
    public CurrencyInfo? CurrencyInfo { get; set; }
    public string? PhotoPath { get; set; }
    
    public static PhoneDetailsVm FromModel(Models.Phone phone)
    {
        return new PhoneDetailsVm
        {
            Id = phone.Id,
            Brand = phone.Brand?.Name,
            Price = phone.Price,
            Title = phone.Title,
            Description = phone.Description,
            BatteryCapacity = phone.BatteryCapacity,
            Ram = phone.Ram,
            DisplayType = phone.DisplayType,
            PhotoPath = phone.PhotoPath
        };
    }
}