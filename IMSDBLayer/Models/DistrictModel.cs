using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.Models
{
    public partial class District
    {
        public District() { }
        public District(District d)
        {
            this.Id = d.Id;
            this.Name = d.Name;
        }

    }
}
