using Lesson.Models;
using Lesson.ViewModels.PhoneVms;

namespace Lesson.Mappers;

public static class PhoneMapper
{
    public static PhoneVm ToVm(this Phone phone)
    {
        return new PhoneVm
        {
            Id = phone.Id,
            Price = phone.Price,
            Title = phone.Title,
            BrandName = phone.Brand!.Name
        };
    }
}