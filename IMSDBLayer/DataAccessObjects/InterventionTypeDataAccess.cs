using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;
using IMSDBLayer.DataAccessInterfaces;

namespace IMSDBLayer.DataAccessObjects
{
    public class InterventionTypeDataAccess : IInterventionTypeDataAccess
    {
        public InterventionTypeDataAccess() { }
        public InterventionType create(InterventionType interventionType)
        {
            using (IMSEntities context = new IMSEntities())
            {
                context.InterventionTypes.Add(new InterventionType(interventionType));
                context.SaveChanges();
                return context.InterventionTypes.Find(interventionType);

            }
        }

        public InterventionType fetchInterventionTypesById(Guid interventionTypeId)
        {
            using (IMSEntities context = new IMSEntities())
            {


                return context.InterventionTypes.Where(i => i.Id == interventionTypeId).FirstOrDefault();

            }
        }

        public IEnumerable<InterventionType> getAll()
        {
            using (IMSEntities context = new IMSEntities())
            {


                return context.InterventionTypes.ToList();

            }
        }

        public bool update(InterventionType interventionType)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var old = context.InterventionTypes.Where(i => i.Id == interventionType.Id).FirstOrDefault();
                context.Entry(old).CurrentValues.SetValues(interventionType);
                if (context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
