namespace Lesson.Options;

public static class StaticClass 
{
    public static int Test;
    
    static StaticClass()
    {
        Test = 10;
        Console.WriteLine("Привет я статика");    
    }
}