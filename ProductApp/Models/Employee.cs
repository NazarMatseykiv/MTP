using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        public int Age { get; set; }
    }
}