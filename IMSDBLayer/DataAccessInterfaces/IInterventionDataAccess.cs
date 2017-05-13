using IMSDBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.DataAccessInterfaces
{
    public interface IInterventionDataAccess
    {
        /// <summary>
        /// Create an intervention
        /// </summary>
        /// <param name="intervention">an intervention object</param>
        /// <returns>an intervention object created</returns>
        Intervention create(Intervention intervention);
        /// <summary>
        /// Update the intervention
        /// </summary>
        /// <param name="intervention">intervention object</param>
        /// <returns>intervention object</returns>
        bool update(Intervention intervention);
        /// <summary>
        /// Get all interventions
        /// </summary>
        /// <returns>a list of interventions</returns>
        IEnumerable<Intervention> getAll();
        /// <summary>
        /// Get a list of intervention by it's clientId
        /// </summary>
        /// <param name="clientId">the guid of the client</param>
        /// <returns>a list of intervention</returns>
        IEnumerable<Intervention> fetchInterventionsByClientId(Guid clientId);
        /// <summary>
        /// Get an intervention by it's id
        /// </summary>
        /// <param name="interventionId">the guid of an intervention</param>
        /// <returns>intervention object</returns>
        Intervention fetchInterventionsById(Guid interventionId);
        /// <summary>
        /// Get a list of intervention by it's state
        /// </summary>
        /// <param name="state">The state of the intervention</param>
        /// <returns>a list of intervention</returns>
        IEnumerable<Intervention> fetchInterventionsByState(int state);
        /// <summary>
        /// Get a list of intervention by it's district
        /// </summary>
        /// <param name="districtId">the guid of a district</param>
        /// <returns>a list of intervention</returns>
        IEnumerable<Intervention> fetchInterventionsByDistrict(Guid districtId);
        /// <summary>
        ///  Get a list of intervention by it's interventionTypeId
        /// </summary>
        /// <param name="interventionTypeId">the guid of interventionType</param>
        /// <returns> a list of intervention</returns>
        IEnumerable<Intervention> fetchInterventionsByInterventionType(Guid interventionTypeId);
        /// <summary>
        /// Get a list of intervention by it's createdBy
        /// </summary>
        /// <param name="createdBy">the guid of the creator</param>
        /// <returns>a list of intervention</returns>
        IEnumerable<Intervention> fetchInterventionsByCreator(Guid creatorId);
        /// <summary>
        /// Get a list of intervention by it's approved userId
        /// </summary>
        /// <param name="userId">the guid of an user</param>
        /// <returnsa list of intervention></returns>
        IEnumerable<Intervention> fetchInterventionsByApprovedUser(Guid userId);
    }
}
