using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SecTask.Models
{
    public class PatientView
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите имя")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z]+$", ErrorMessage = "Имя может содержать только буквы")]
        public string? Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z]+$", ErrorMessage = "Фамилия может содержать только буквы")]
        public string? Surname { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Вес, кг")]
        [Required(ErrorMessage = "Введите вес в килограммах")]
        public int Weight { get; set; }

        [Display(Name = "Рост, см")]
        [Required(ErrorMessage = "Введите рост в сантиметрах")]
        public int Height { get; set; }
    }
}
