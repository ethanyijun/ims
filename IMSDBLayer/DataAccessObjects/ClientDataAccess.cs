using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;
using IMSDBLayer.DataAccessInterfaces;

namespace IMSDBLayer.DataAccessObjects
{
    public class ClientDataAccess : IClientDataAccess
    {
        public ClientDataAccess() { }

        public Client createClient(Client client)
        {
            using (IMSEntities context = new IMSEntities())
            {
                context.Clients.Add(client);
                context.SaveChanges();
                return context.Clients.Find(client);
            }

        }

        public Client fetchClientById(Guid clientId)
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            }
        }

        public IEnumerable<Client> fetchClientsByDistrictId(Guid districtId)
        {
            IEnumerable<Client> items = null;

            using (IMSEntities context = new IMSEntities())
            {
                items= (IEnumerable<Client>)context.Clients.Where(c => c.DistrictId == districtId).ToList();
                // return context.Clients.Where(c => c.DistrictId == districtId).AsEnumerable();
            }
            return items;
        }

        public IEnumerable<Client> getAll()
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Clients.ToList();
            }
        }

        public bool updateClient(Client client)
        {
            using (IMSEntities context = new IMSEntities())
            {
                //var old = context.Clients.Where(c => c.Id == client.Id).FirstOrDefault();
                var old = context.Clients.Find(client);
                old = client;

                if (context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
