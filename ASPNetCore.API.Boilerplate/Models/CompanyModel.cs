using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.API.Boilerplate.Models
{
    public class CompanyModel
    {
        [Key]
        public Guid CompanyID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<EmployeeModel> Employees { get; set; }
    }
}
