using System.ComponentModel.DataAnnotations;

namespace OJTApp.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string CivilStatus { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
