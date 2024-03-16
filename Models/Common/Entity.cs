namespace Lesson.Models.Common;

public abstract class Entity
{
    public long Id { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj != null && obj.GetType() == GetType())
        {
            return ((Entity) obj).Id == Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}