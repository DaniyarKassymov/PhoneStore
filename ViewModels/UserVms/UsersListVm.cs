using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson.ViewModels.UserVms;

public class UsersListVm
{
    public required IEnumerable<UserVm> Users { get; set; } = new List<UserVm>();
    public required SelectList Brands { get; set; }
    public string? Name { get; set; }
    public required PageVm PageVm { get; set; }
}