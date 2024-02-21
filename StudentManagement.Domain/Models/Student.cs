using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Domain.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string? StudentName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Age must be a positive value.")]
        public int Age { get; set; }

        public char? Grade { get; set; }
    }
}
