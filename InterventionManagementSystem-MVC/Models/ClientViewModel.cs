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
        public string Name { get; set; }
        public string Location { get; set; }
        public string DistrictName { get; set; }
    }
}