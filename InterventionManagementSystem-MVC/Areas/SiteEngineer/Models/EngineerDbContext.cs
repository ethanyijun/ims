using IMSLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer.Models
{
    public class EngineerDbContext:DbContext
    {
        public EngineerDbContext() : base("DefaultConnection") {

        }
        public DbSet <User> User { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Client> Clients { get; set; }

        public System.Data.Entity.DbSet<InterventionManagementSystem_MVC.Areas.SiteEngineer.Models.SiteEngineer> SiteEngineers { get; set; }
    }
}