using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterventionManagementSystem_MVC.Models
{
    public class ClientViewModel
    {
        [Key]
        public Nullable<Guid> Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Min=3 Max = 30")]
        public string Name { get; set; }

        [Required]

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Min=3 Max = 50")]
        public string Location { get; set; }

        public string DistrictName { get; set; }
    }
}