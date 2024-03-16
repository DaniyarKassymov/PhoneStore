using System.ComponentModel.DataAnnotations;

namespace Lesson.Enums;

public enum SidesOfTheWorld
{
    [Display(Name = "Север")] North,
    [Display(Name = "Юг")] South,
    [Display(Name = "Запад")] West,
    [Display(Name = "Восток")] East
}