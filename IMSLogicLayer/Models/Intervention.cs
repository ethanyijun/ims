using IMSLogicLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.Models
{
    public class Intervention : IMSDBLayer.Models.Intervention
    {
        private Client client;
        private District district;
        private InterventionType interventionType;

        public Intervention(decimal hours, decimal costs, int lifeRemaining, string comments, InterventionState state, DateTime dateCreate, DateTime? dateFinish, DateTime dateRecentVisit, Guid interventionTypeId, Guid clientId, Guid createdBy, Guid? approvedBy)
        {
            Hours = hours;
            Costs = costs;
            LifeRemaining = lifeRemaining;
            Comments = comments;
            base.State = (int)state;

            DateCreate = dateCreate;
            DateFinish = dateFinish;

            DateRecentVisit = dateRecentVisit;
            InterventionTypeId = interventionTypeId;
            ClientId = clientId;
            CreatedBy = createdBy;
            ApprovedBy = approvedBy;
        }

        public Intervention(IMSDBLayer.Models.Intervention intervention)
        {
            base.Id = intervention.Id;
            base.Hours = intervention.Hours;
            base.Costs = intervention.Costs;
            base.LifeRemaining = intervention.LifeRemaining;
            base.Comments = intervention.Comments;
            base.State = intervention.State;

            base.DateCreate = intervention.DateCreate;
            base.DateFinish = intervention.DateFinish;

            base.DateRecentVisit = intervention.DateRecentVisit;
            base.InterventionTypeId = intervention.InterventionTypeId;
            base.ClientId = intervention.ClientId;
            base.CreatedBy = intervention.CreatedBy;
            base.ApprovedBy = intervention.ApprovedBy;
        }

        public Client Client
        {
            get { return client; }
            set { this.client = value; }
        }

        public District District
        {
            get { return district; }
            set { this.district = value; }
        }

        public InterventionType InterventionType
        {
            get { return interventionType; }
            set { this.interventionType = value; }
        }

        public InterventionState InterventionState
        {
            get { return (InterventionState)base.State; }
            set { base.State = (int)value; }
        }
    }
}
