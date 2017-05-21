using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;
using IMSDBLayer.DataAccessInterfaces;

namespace IMSDBLayer.DataAccessObjects
{
    public class DistrictDataAccess : IDistrictDataAccess
    {

        public DistrictDataAccess() { }
        public District create(District district)
        {
            using (IMSEntities context = new IMSEntities())
            {

                context.Districts.Add(district);
                context.SaveChanges();
                return context.Districts.Find(district);

            }
        }

        public District fetchDistrictById(Guid districtId)
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Districts.Where(d => d.Id == districtId).FirstOrDefault();
            }
        }

        public District fetchDistrictByName(string name)
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Districts.Where(d => d.Name == name).FirstOrDefault();
            }
        }

        public IEnumerable<District> getAll()
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Districts.ToList();
            }
        }

        public bool update(District district)
        {
            using (IMSEntities context = new IMSEntities())
            {
                //var old = context.Clients.Where(c => c.Id == client.Id).FirstOrDefault();
                var old = context.Districts.Find(district);
                old = district;

                if (context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
