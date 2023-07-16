using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecTask.Models;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace SecTask.Controllers
{
    public class BMIController : Controller
    {
        private readonly PatientDbContext _dbContext;

        public BMIController(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Patient()
        {
            return View();
        }

        public IActionResult Statistics()
        {
            return View();
        }


        [HttpGet("CalculateBMI")]
        public IActionResult CalculateBMI(float weight, float height)
        {
            if (height <= 0 || weight <= 0 || height >= 260 || weight >= 620)
            {
                return BadRequest();
            }

            double bmi = BMI(weight, height);
            string description = DescriptionBMI(bmi);

            var result = new
            {
                BMI = (float)bmi,
                Description = description
            };
            
            return Ok(result);
        }


        [HttpPost("AddPatient")]
        public IActionResult AddPatient(PatientView patient)
        {
            if (ModelState.IsValid)
            {
                if (TestInputData(patient) != "Ok")
                {
                    return Json(new { success = false, message = TestInputData(patient) });
                }
                
                double bmi = BMI(patient.Weight, patient.Height);
                string description = DescriptionBMI(bmi);

                var PatientRecord = new Patient
                {
                    Surname = patient.Surname,
                    Name = patient.Name,
                    Age = patient.Age,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    BMI = (float)bmi,
                    Description = description
                };

                _dbContext.patient.Add(PatientRecord);
                _dbContext.SaveChanges();

                string message = "Пациент успешно добавлен в базу данных.";
                return Json(new { success = true, message = message });
            }
            else
            {
                string errorMessage = "Заполните все поля правильно.";
                return Json(new { success = false, message = errorMessage });
            }
        }

        [HttpGet("GetStatistics")]
        public IActionResult GetStatistics()
        {
            var bmiStatistics = _dbContext.patient
                .GroupBy(p => p.Description)
                .Select(g => new BMIData
                {
                    Description = g.Key,
                    Percentage = ((double)g.Count() / _dbContext.patient.Count() * 100)
                })
                .OrderByDescending(b => b.Percentage)
                .ToList();
            
            foreach (var bmiData in bmiStatistics)
            {
                if (BMIDataView.data.ContainsKey(bmiData.Description))
                {
                    BMIDataView.data[bmiData.Description] = bmiData.Percentage;
                }
            }

            return Ok(BMIDataView.data.OrderByDescending(kv => kv.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        [HttpGet("GetStatisticsAge")]
        public IActionResult GetStatisticsAge()
        {
            var bmiStatisticsAge 
                = _dbContext.BMIDataAge.FromSqlRaw("SELECT * FROM GetBMIStatisticsByAge()").ToList();

            foreach (var bmiData in bmiStatisticsAge)
            {
                if (BMIDataAgeView.dataView.ContainsKey(bmiData.AgeRange))
                {
                    BMIDataAgeView.dataView[bmiData.AgeRange]
                        [bmiData.BMICharacteristic] = (double)bmiData.Percentage;
                }
            }

            return Ok(BMIDataAgeView.dataView);
        }


        public double BMI(float weight, float height)
        {
            return weight / Math.Pow((double)height / 100, 2);
        }

        public string DescriptionBMI(double bmi)
        {
            if (bmi < 18.5)
            {
                return "Недостаточный вес";
            }
            else if (bmi < 25)
            {
                return "Нормальный вес";
            }
            else if (bmi < 30)
            {
                return "Избыточный вес";
            }
            return "Ожирение";
        }

        public string TestInputData(PatientView patient)
        {
            if (patient.Weight <= 0)
            {
                return "Вес слишком маленький.";
            }

            if (patient.Height <= 0)
            {
                return "Рост слишком низкий.";
            }

            if (patient.Weight >= 620)
            {
                return "Вес слишком большой.";
            }

            if (patient.Height >= 260)
            {
                return "Рост слишком маленький.";
            }

            if (patient.Age <= 0 || patient.Age >= 125)
            {
                return "Некорректный возраст.";
            }

            return "Ok";
        }

    }

}
