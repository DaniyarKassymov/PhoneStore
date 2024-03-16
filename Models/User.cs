using Lesson.Models.Common;

namespace Lesson.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public long? BrandId { get; set; }
    public Brand? Brand { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserAuthRole? UserAuthRole { get; set; }
    public int? UserAuthRoleId { get; set; }
}