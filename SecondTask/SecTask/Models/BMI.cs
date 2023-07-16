using System.ComponentModel.DataAnnotations;

namespace SecTask.Models
{
    public class BMI
    {
        [Display(Name = "Вес, кг")]
        [Required(ErrorMessage = "Введите вес в килограммах")]
        public float Weight { get; set; }

        [Display(Name = "Рост, см")]
        [Required(ErrorMessage = "Введите рост в сантиметрах")]
        public float Height { get; set; }
    }
}
