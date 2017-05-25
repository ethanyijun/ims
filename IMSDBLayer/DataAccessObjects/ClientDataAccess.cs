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
                context.Clients.Add(new Client(client));
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
            using (IMSEntities context = new IMSEntities())
            {

                return context.Clients.Where(c => c.DistrictId == districtId).ToList();
            }
        }

        public IEnumerable<Client> getAll()
        {
            using (IMSEntities context = new IMSEntities())
            {

                return context.Clients.AsEnumerable();
            }
        }

        public bool updateClient(Client client)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var old = context.Clients.Where(c => c.Id == client.Id).FirstOrDefault();
               
                old = new Client(client);

               
                if (context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
