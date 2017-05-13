using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;

namespace IMSDBLayer.DataAccessInterfaces
{
    public interface IClientDataAccess
    {
        /// <summary>
        /// Create a Client
        /// </summary>
        /// <param name="client">Client object</param>
        /// <returns>Client object created</returns>
        Client createClient(Client client);
        /// <summary>
        /// Update a Client
        /// </summary>
        /// <param name="client">client object</param>
        /// <returns>true if success, false if fail</returns>
        bool updateClient(Client client);
        /// <summary>
        /// Get all Client
        /// </summary>
        /// <returns>A list of Client</returns>
        IEnumerable<Client> getAll();
        /// <summary>
        /// Get a client by it's id
        /// </summary>
        /// <param name="clientId">the guid of Client</param>
        /// <returns>Client object</returns>
        Client fetchClientById(Guid clientId);
        /// <summary>
        /// Get all clients in the district
        /// </summary>
        /// <param name="districtId">The guid of a district</param>
        /// <returns>A list of Client</returns>
        IEnumerable<Client> fetchClientsByDistrictId(Guid districtId);

    }
}
