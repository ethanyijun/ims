using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.Models
{
    public class Client : IMSDBLayer.Models.Client
    {
        public Client(string name, string location, Guid districtId)
        {
            Name = name;
            Location = location;
            DistrictId = districtId;
        }

        public Client(IMSDBLayer.Models.Client client)
        {
            base.Id = client.Id;
            base.Name = client.Name;
            base.Location = client.Location;
            base.DistrictId = client.DistrictId;
        }
    }
}
