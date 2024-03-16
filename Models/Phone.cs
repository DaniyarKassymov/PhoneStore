using Lesson.Models.Common;

namespace Lesson.Models;

public class Phone : Entity
{
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public string? DisplayType { get; set; }
    public short? Ram { get; set; }
    public short? BatteryCapacity { get; set; }
    public string? Description { get; set; }
    
    public string? PhotoPath { get; set; }
    public long? BrandId { get; set; }
    public Brand? Brand { get; set; }
}