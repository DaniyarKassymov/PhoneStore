using Lesson.Models.Common;
using Lesson.ViewModels;
using Lesson.ViewModels.BrandVms;

namespace Lesson.Models;

public class Brand : Entity
{
    public string? Name { get; set; }
    public string? Link { get; set; }

    public List<User>? Users { get; set; } = new();
    
    public BrandVm ToViewModel()
    {
        return new BrandVm
        {
            Name = Name,
            Link = Link,
            Id = Id
        };
    }
}