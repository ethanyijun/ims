﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMSDBLayer.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IMSEntities : DbContext
    {
        public IMSEntities()
            : base("name=IMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<InterventionType> InterventionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        object placeHolderVariable;
    }
}
