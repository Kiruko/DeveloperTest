﻿using System.ComponentModel.DataAnnotations;

namespace SecTask.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public float BMI { get; set; }
        public string? Description { get; set; }
    }

}
