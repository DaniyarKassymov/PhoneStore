using System.ComponentModel.DataAnnotations;

namespace Lesson.ViewModels.UserAuthVms;

public class LoginVm
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email адрес")]
    [Required(ErrorMessage = "Это поля обязательно к заполнению")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Это поле обязательно к заполнению")]
    public string Password { get; set; }
}