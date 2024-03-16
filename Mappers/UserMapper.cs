using Lesson.Models;
using Lesson.ViewModels.UserVms;

namespace Lesson.Mappers;

public static class UserMapper
{
    public static UserVm ToVm(this User user)
    {
        return new UserVm
        {
            Age = user.Age,
            Name = user.Name,
            BrandName = user.Brand?.Name,
            Password = user.Password!,
            BrandId = user.BrandId,
            UserAuthRoleId = user.UserAuthRoleId,
            Email = user.Email,
        };
    }
}