using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.Models
{
    public class InterventionType: IMSDBLayer.Models.InterventionType
    {
        public InterventionType(string name, decimal hours, decimal costs)
        {
            base.Name = name;
            base.Hours = hours;
            base.Costs = costs;
        }

        public InterventionType(IMSDBLayer.Models.InterventionType interventionType)
        {
            base.Id = interventionType.Id;
            base.Name = interventionType.Name;
            base.Hours = interventionType.Hours;
            base.Costs = interventionType.Costs;
        }
    }
}
