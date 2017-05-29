using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.Models
{

    public partial class Client
    {

        public Client() { }
        public Client(Client Client)
        {
            this.Id = Client.Id;
            this.Name = Client.Name;
            this.Location = Client.Location;
            this.DistrictId = Client.DistrictId;
        }

    }
}
