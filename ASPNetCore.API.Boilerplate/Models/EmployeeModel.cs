using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.API.Boilerplate.Models
{
    public class EmployeeModel
    {
        [Key]
        public string EmployeeID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
