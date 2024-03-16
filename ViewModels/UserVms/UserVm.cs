using Lesson.Models;

namespace Lesson.ViewModels.UserVms;

public class UserVm
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public long? BrandId { get; set; }
    public string? BrandName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int? UserAuthRoleId { get; set; }
}