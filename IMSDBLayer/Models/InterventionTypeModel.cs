using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.Models
{
    public partial class InterventionType
    {
        public InterventionType() { }
        public InterventionType(InterventionType i)
        {
            this.Costs = i.Costs;
            this.Hours = i.Hours;
            this.Id = i.Id;
            this.Name = i.Name;
        }
    }
}
