using IMSLogicLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSLogicLayer.Enums;
using IMSLogicLayer.Models;

namespace IMSLogicLayer.Services
{
    public class InterventionService : BaseService, IInterventionService
    {
        public InterventionService() : base()
        {
        }
        /// <summary>
        /// Get A list of interventions based on its creator
        /// </summary>
        /// <param name="creatorId">The user guid who created the intervention</param>
        /// <returns>A list of interventions created by a particular user</returns>
        public IEnumerable<Intervention> getInterventionsByCreatorId(Guid creatorId)
        {
            return Interventions.fetchInterventionsByCreator(creatorId).Select(c => new Intervention(c)).ToList();
        }
        /// <summary>
        /// Get a particular intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <returns>An intervention</returns>
        public Intervention getInterventionsById(Guid interventionId)
        {
            return new Intervention(Interventions.fetchInterventionsById(interventionId));
        }
        /// <summary>
        /// Get A list of intervention which state is proposed
        /// </summary>
        /// <returns>A list of proposed intervention</returns>
        public IEnumerable<Intervention> getListofProposedInterventions()
        {
            return Interventions.fetchInterventionsByState((int)InterventionState.Proposed).Select(c => new Intervention(c)).ToList();
        }
        /// <summary>
        /// Get intervention list of a particular client
        /// </summary>
        /// <param name="clientId">The guid of a client</param>
        /// <returns>A list of intervention</returns>
        public IEnumerable<Intervention> getInterventionsByClientId(Guid clientId)
        {
            return Interventions.fetchInterventionsByClientId(clientId).Select(c => new Intervention(c)).ToList();
        }
        /// <summary>
        /// Update intervention quality information
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="comments">The comments for an intervention</param>
        /// <param name="remainLife">The remaining life of an intervention</param>
        /// <param name="lastVisitDate">The recent visit date of an intervention</param>
        /// <returns>True if success, False if fail</returns>
        public bool updateInterventionDetail(Guid interventionId, string comments, int remainLife, DateTime lastVisitDate)
        {
            Intervention intervention = getInterventionsById(interventionId);
            intervention.Comments = comments;
            intervention.LifeRemaining = remainLife;
            intervention.DateRecentVisit = lastVisitDate;

            return Interventions.update(intervention);
        }
        /// <summary>
        /// Update the recent visit date of an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="lastVisitDate">The date update to</param>
        /// <returns>True if success, false if fail</returns>
        public bool updateInterventionLastVisitDate(Guid interventionId, DateTime lastVisitDate)
        {
            Intervention intervention = getInterventionsById(interventionId);
            intervention.DateRecentVisit = lastVisitDate;
            return Interventions.update(intervention);

        }
        /// <summary>
        /// Update the intervention State
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="state">The state update to</param>
        /// <returns>True if success, false if fail</returns>
        public bool updateInterventionState(Guid interventionId, InterventionState state)
        {
            Intervention intervention = getInterventionsById(interventionId);
            //complete or cancelled intervention can't update state
            if (intervention.InterventionState !=InterventionState.Completed && intervention.InterventionState!=InterventionState.Cancelled)
            {
                //state propose can't go straight to complete
                if (intervention.InterventionState ==InterventionState.Proposed)
                {
                    if (state==InterventionState.Completed)
                    {
                        return false;
                    }
                    else
                    {
                        intervention.InterventionState = state;
                        
                    }
                }else //original = Approved
                {
                    //approved can only go into complete or cancelled which will set the datefinish as now
                    if(state != InterventionState.Proposed && state!=InterventionState.Approved)
                    {
                        intervention.InterventionState = state;
                        intervention.DateFinish = DateTime.Now;
                    }else
                    {
                        return false;
                    }
                }
                
            }else
            {
                return false;
            }
            
            return Interventions.update(intervention);
        }
        /// <summary>
        /// Update the approve by property of an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention </param>
        /// <param name="user">The user instance which updated the intervention</param>s
        /// <returns></returns>
        public bool updateIntervetionApprovedBy(Guid interventionId, User user)
        {
            Intervention intervention = getInterventionsById(interventionId);
            intervention.ApprovedBy = user.Id;
            return Interventions.update(intervention);
        }
        /// <summary>
        /// Update the intervention state 
        /// </summary>
        /// <param name="interventionId">The guid of the intervention</param>
        /// <param name="state">The state which update to</param>
        /// <param name="userId">The user guid who update the intervention</param>
        /// <returns>True if successfully updated the intervention state, false if fail</returns>
        public bool updateInterventionState(Guid interventionId, InterventionState state, Guid userId)
        {
            if (updateInterventionState(interventionId,state))
            {
                var intervention = getInterventionsById(interventionId);
                intervention.ApprovedBy = userId;
                return Interventions.update(intervention);
            }else
            {
                return false;
            }
        }
        /// <summary>
        /// Get the list of interventions based on the user approved it
        /// </summary>
        /// <param name="userId">The user guid which approved the intervention</param>
        /// <returns>A list of interventions</returns>
        public IEnumerable<Intervention> getInterventionsByApprovedUser(Guid userId)
        {
            return Interventions.fetchInterventionsByApprovedUser(userId).Select(c => new Intervention(c)).ToList();
        }
    }
}
