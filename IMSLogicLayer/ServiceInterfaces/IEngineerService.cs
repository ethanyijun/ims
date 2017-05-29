using IMSLogicLayer.Enums;
using IMSLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.ServiceInterfaces
{
    public interface IEngineerService
    {
        
        Guid EngineerIdentityId { get; set; }
        /// <summary>
        /// Get All intervention Types
        /// </summary>
        /// <returns>List of intervention type</returns>
        List<InterventionType> getInterventionTypes();

        /// <summary>
        /// Create a client from name and location
        /// </summary>
        /// <param name="clientName">Name of a client</param>
        /// <param name="clientLocation">Location of a client</param>
        /// <returns>A client instance created</returns>
        Client createClient(string clientName, string clientLocation);

        /// <summary>
        /// Get the instance of current user
        /// </summary>
        /// <returns>The current user instance</returns>
        User getDetail();
        /// <summary>
        /// Get the User instance by its id
        /// </summary>
        /// <param name="userId">The guid of an user</param>
        /// <returns>An user instance</returns>
        User getUserById(Guid userId);

        /// <summary>
        /// Get all clients on the same district as the current user
        /// </summary>
        /// <returns>A list of client</returns>
        IEnumerable<Client> getClients();

        /// <summary>
        /// Get all the interventions for a client
        /// </summary>
        /// <param name="clientId">The guid of a client</param>
        /// <returns>A list of interventions</returns>
        IEnumerable<Intervention> getInterventionsByClient(Guid clientId);

        /// <summary>
        /// Get an client instance from its id
        /// </summary>
        /// <param name="clientId">The guid of a client</param>
        /// <returns>An instance of client</returns>
        Client getClientById(Guid clientId);

        /// <summary>
        /// Get an intervention form it's id
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <returns>The intervention instance</returns>
        Intervention getInterventionById(Guid interventionId);

        /// <summary>
        /// Create an intervention
        /// </summary>
        /// <param name="intervention">An intervention instance</param>
        /// <returns>An instance of Intervention created</returns>
        Intervention createIntervention(Intervention intervention);

        /// <summary>
        /// Get a list of interventions based on it's creator
        /// </summary>
        /// <param name="userId">The guid of an user</param>
        /// <returns>A list of intervention</returns>
        IEnumerable<Intervention> getInterventionListByCreator(Guid userId);
        /// <summary>
        /// Get a list of interventions created and approved by this user
        /// </summary>
        /// <param name="userId">The guid of an user</param>
        /// <returns>A list of interventions</returns>
        IEnumerable<Intervention> getInterventionListByUserId(Guid userId);
        /// <summary>
        /// Update the state property of an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="state">The state of an intervention</param>
        /// <returns>True if success, false if fail</returns>
        bool updateInterventionState(Guid interventionId, InterventionState state);
        /// <summary>
        /// Update approve by property of an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="userId">The guid of an user</param>
        /// <returns>True if success, false if fail</returns>
        bool updateInterventionApproveBy(Guid interventionId, Guid userId);
        /// <summary>
        /// Update the quality information of the intervention, comments, remainlife, recentvisit date
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <param name="comments">The comments of an intervention</param>
        /// <param name="remainLife">The remaining life of an intervention</param>
        /// <param name="lastVisitDate">The recent visit date of an intervention</param>
        /// <returns>True if success, false if fail</returns>
        bool updateInterventionDetail(Guid interventionId, string comments, int remainLife, DateTime lastVisitDate);

        /// <summary>
        /// Approve an intervention
        /// </summary>
        /// <param name="interventionId">The guid of an intervention</param>
        /// <returns>True if success, false if fail</returns>
        bool approveAnIntervention(Guid interventionId);
        

    }
}
