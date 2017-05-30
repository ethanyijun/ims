using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.Models
{
    public partial class Intervention
    {

        public Intervention() { }
        public Intervention(Intervention i)
        {
            this.Id = i.Id;
            this.State = i.State;
            this.LifeRemaining = i.LifeRemaining;
            this.InterventionTypeId = i.InterventionTypeId;
            this.Hours = i.Hours;
            this.DateRecentVisit = i.DateRecentVisit;
            this.DateFinish = i.DateFinish;
            this.DateCreate = i.DateCreate;
            this.CreatedBy = i.CreatedBy;
            this.Costs = i.Costs;
            this.Comments = i.Comments;
            this.ClientId = i.ClientId;
            this.ApprovedBy = i.ApprovedBy;
        }
    }
}
