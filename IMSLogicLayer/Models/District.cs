using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.Models
{
    public class District : IMSDBLayer.Models.District
    {
        public District(string name)
        {
            Name = name;
        }

        public District(IMSDBLayer.Models.District district)
        {
            base.Id = district.Id;
            base.Name = district.Name;
        }
    }
}
