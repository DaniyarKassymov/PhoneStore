using Lesson.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.ViewModels.BrandVms;

public class AddBrandVm
{
    
    [Remote(
        action:"CheckName", 
        controller:"Validation", 
        ErrorMessage = "Этот бренд уже существует")]
    public string Name { get; set; }
    public string Link { get; set; }

    public Brand ToModel()
    {
        return new Brand
        {
            Link = Link,
            Name = Name
        };
    }
}