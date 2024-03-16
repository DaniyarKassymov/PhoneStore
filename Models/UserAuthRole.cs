namespace Lesson.Models;

public class UserAuthRole
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<User> UsersAuth { get; set; }
}