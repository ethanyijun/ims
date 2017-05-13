using IMSDBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.DataAccessInterfaces
{
    public interface IInterventionTypeDataAccess
    {
        /// <summary>
        /// Create an intervention type
        /// </summary>
        /// <param name="interventionType">Intervention type object</param>
        /// <returns>An intervention type object created</returns>
        InterventionType create(InterventionType interventionType);
        /// <summary>
        /// Update an intervention type 
        /// </summary>
        /// <param name="interventionType">Intervention type object</param>
        /// <returns>True if success, false if fail</returns>
        bool update(InterventionType interventionType);
        /// <summary>
        /// Get all intervention type
        /// </summary>
        /// <returns>A list of intervention type</returns>
        IEnumerable<InterventionType> getAll();
        /// <summary>
        /// Get intervention type by it's id
        /// </summary>
        /// <param name="interventionTypeId">The guid of an intervention type</param>
        /// <returns>An intervention type object</returns>
        InterventionType fetchInterventionTypesById(Guid interventionTypeId);
    }
}
