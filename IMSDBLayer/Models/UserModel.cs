using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.Models
{
    public partial class User
    {
        public User() { }
        public User(User u)
        {
            this.AuthorisedCosts = u.AuthorisedCosts;
            this.AuthorisedHours = u.AuthorisedHours;
            this.DistrictId = u.DistrictId;
            this.Id = u.Id;
            this.IdentityId = u.IdentityId;
            this.Name = u.Name;
            this.Type = u.Type;
        }
    }
}
