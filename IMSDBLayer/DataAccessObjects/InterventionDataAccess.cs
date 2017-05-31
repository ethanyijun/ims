using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;
using IMSDBLayer.DataAccessInterfaces;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace IMSDBLayer.DataAccessObjects
{
    public class InterventionDataAccess : IInterventionDataAccess
    {
        public InterventionDataAccess() { }
        public Intervention create(Intervention intervention)
        {
            using (IMSEntities context = new IMSEntities())
            {

                context.Interventions.Add(new Intervention(intervention));
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                return (Intervention)context.Interventions.Find(intervention.Id);
              //  return intervention;
            }
        }

        public IEnumerable<Intervention> fetchInterventionsByApprovedUser(Guid userId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.Where(i => i.ApprovedBy == userId).ToList();
            }
        }

        public IEnumerable<Intervention> fetchInterventionsByClientId(Guid clientId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.Where(i => i.ClientId == clientId).ToList();
            }
        }

        public IEnumerable<Intervention> fetchInterventionsByCreator(Guid creatorId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.Where(i => i.CreatedBy == creatorId).ToList();
            }
        }

        public IEnumerable<Intervention> fetchInterventionsByDistrict(Guid districtId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var clientId = context.Clients.Where(c => c.DistrictId == districtId).FirstOrDefault().Id;
                return context.Interventions.Where(i => i.ClientId == clientId).ToList();
            }
        }

        public Intervention fetchInterventionsById(Guid interventionId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.Where(i => i.Id == interventionId).FirstOrDefault();
            }
        }
        //public IEnumerable<Intervention> fetchInterventionsListById(Guid interventionId)
        //{
        //    using (IMSEntities context = new IMSEntities())
        //    {
        //        return context.Interventions.Where(i => i.Id == interventionId).ToList();
        //    }
        //}


        public IEnumerable<Intervention> fetchInterventionsByInterventionType(Guid interventionTypeId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.Where(i => i.InterventionTypeId == interventionTypeId).ToList();
            }
        }

        public IEnumerable<Intervention> fetchInterventionsByState(int state)
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.Where(i => i.State == state).ToList();
            }
        }

        public IEnumerable<Intervention> getAll()
        {
            using (IMSEntities context = new IMSEntities())
            {
                return context.Interventions.ToList();
            }
        }

        public bool update(Intervention intervention)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var old = context.Interventions.Where(i => i.Id == intervention.Id).FirstOrDefault();
                old = new Intervention(intervention);
                if (context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
