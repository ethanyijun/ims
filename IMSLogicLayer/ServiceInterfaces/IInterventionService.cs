using IMSLogicLayer.Enums;
using IMSLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.ServiceInterfaces
{
    public interface IInterventionService
    {
        /// <summary>
        /// Get a particular intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <returns>An intervention</returns>
        Intervention getInterventionsById(Guid interventionId);
        /// <summary>
        /// Get A list of intervention which state is proposed
        /// </summary>
        /// <returns>A list of proposed intervention</returns>
        IEnumerable<Intervention> getListofProposedInterventions();
        /// <summary>
        /// Get A list of interventions based on its creator
        /// </summary>
        /// <param name="creatorId">The user guid who created the intervention</param>
        /// <returns>A list of interventions created by a particular user</returns>
        IEnumerable<Intervention> getInterventionsByCreatorId(Guid creatorId);
        /// <summary>
        /// Get intervention list of a particular client
        /// </summary>
        /// <param name="clientId">The guid of a client</param>
        /// <returns>A list of intervention</returns>
        IEnumerable<Intervention> getInterventionsByClientId(Guid clientId);
        /// <summary>
        /// Get the list of interventions based on the user approved it
        /// </summary>
        /// <param name="userId">The user guid which approved the intervention</param>
        /// <returns>A list of interventions</returns>
        IEnumerable<Intervention> getInterventionsByApprovedUser(Guid userId);
        /// <summary>
        /// Update the intervention State
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="state">The state update to</param>
        /// <returns>True if success, false if fail</returns>
        bool updateInterventionState(Guid interventionId, InterventionState state);
        /// <summary>
        /// Update intervention quality information
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="comments">The comments for an intervention</param>
        /// <param name="remainLife">The remaining life of an intervention</param>
        /// <param name="lastVisitDate">The recent visit date of an intervention</param>
        /// <returns>True if success, False if fail</returns>
        bool updateInterventionDetail(Guid interventionId, string comments, int remainLife, DateTime lastVisitDate);
        /// <summary>
        /// Update the recent visit date of an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="lastVisitDate">The date update to</param>
        /// <returns>True if success, false if fail</returns>
        bool updateInterventionLastVisitDate(Guid interventionId, DateTime lastVisitDate);
        /// <summary>
        /// Update the approve by property of an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention </param>
        /// <param name="user">The user instance which updated the intervention</param>s
        /// <returns></returns>
        bool updateIntervetionApprovedBy(Guid interventionId, User user);
        /// <summary>
        /// Update the intervention state 
        /// </summary>
        /// <param name="interventionId">The guid of the intervention</param>
        /// <param name="state">The state which update to</param>
        /// <param name="userId">The user guid who update the intervention</param>
        /// <returns>True if successfully updated the intervention state, false if fail</returns>
        bool updateInterventionState(Guid interventionId, InterventionState state, Guid identityId);
    }
}
