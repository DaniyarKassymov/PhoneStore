using Lesson.Models.Common;

namespace Lesson.Models;

public class Order : Entity
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ContactPhone { get; set; }
    
    public long PhoneId { get; set; }
    public Phone Phone { get; set; }
}