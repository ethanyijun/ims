using IMSLogicLayer.Enums;
using IMSLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.ServiceInterfaces
{
    public interface IManagerService
    {
        Guid ManagerId { get; set; }
        IInterventionService InterventionService { get; set; }
        /// <summary>
        /// Get the current User instance
        /// </summary>
        /// <returns>The current user</returns>
        User GetDetail();

        /// <summary>
        /// Get a list of interventions from the state they are in
        /// </summary>
        /// <param name="state">The state of an intervention</param>
        /// <returns>A list of intervention</returns>
        IEnumerable<Intervention> GetInterventionsByState(InterventionState state);
        /// <summary>
        /// Get approved Interventions of the current Manager
        /// </summary>
        /// <returns>A list of Intervention with interventionType, client, district property</returns>
        IEnumerable<Intervention> GetApprovedInterventions();


        /// <summary>
        /// Get an intervention from guid provided
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <returns>The corresponding intervention</returns>
        Intervention GetInterventionById(Guid interventionId);

        /// <summary>
        /// Approve an intervention 
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <returns>True if successfully approved an intervention, false if fail</returns>
        bool ApproveAnIntervention(Guid interventionId);
        /// <summary>
        /// Update the state of an intervention 
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="state">The enum intervention state to update to</param>
        /// <returns>True if successfully updated, false if fail</returns>
        bool UpdateInterventionState(Guid interventionId, InterventionState state);
        /// <summary>
        /// Update Approved by property of the intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="userId">The current user id</param>
        /// <returns>True if success, false if fail</returns>
        bool UpdateInterventionApproveBy(Guid interventionId, Guid userId);

    }
}
