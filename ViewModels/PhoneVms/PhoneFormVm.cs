using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson.ViewModels.PhoneVms;

public class PhoneFormVm
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Поле названия обязательно")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина от 2 до 20")]
    public string Title { get; set; } = string.Empty;
    
    [Range(1, 4000, ErrorMessage = "Сумма должна быть от 1 до 4000")]
    public decimal Price { get; set; }
    public string? DisplayType { get; set; }
    public short? Ram { get; set; }
    public short? BatteryCapacity { get; set; }
    public string? Description { get; set; }
    [Required] public long? BrandId { get; set; }
    public SelectList? Brands { get; set; }
    
    [Required(ErrorMessage = "Загрузите фото")]
    public IFormFile Photo { get; set; }
    
    public Models.Phone ToModel(string path)
    {
        return new Models.Phone
        {
            Id = Id,
            Price = Price,
            BatteryCapacity = BatteryCapacity,
            Title = Title!,
            DisplayType = DisplayType,
            Description = Description,
            BrandId = BrandId,
            Ram = Ram,
            PhotoPath = path
        };
    }

    public static PhoneFormVm FromModel(Models.Phone phone)
    {
        return new PhoneFormVm
        {
            Ram = phone.Ram,
            Description = phone.Description,
            DisplayType = phone.DisplayType,
            BatteryCapacity = phone.BatteryCapacity,
            Title = phone.Title,
            BrandId = phone.BrandId,
            Price = phone.Price,
            Id = phone.Id
        };  
    }
}