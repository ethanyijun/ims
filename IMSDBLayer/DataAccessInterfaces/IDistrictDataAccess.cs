using IMSDBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.DataAccessInterfaces
{
    public interface IDistrictDataAccess
    {
        /// <summary>
        /// Create a district
        /// </summary>
        /// <param name="district">district object</param>
        /// <returns>district object</returns>
        District create(District district);
        /// <summary>
        /// Update a district
        /// </summary>
        /// <param name="district">district object</param>
        /// <returns>True if success, false if fail</returns>
        bool update(District district);
        /// <summary>
        /// Get all district
        /// </summary>
        /// <returns>A list of district</returns>
        IEnumerable<District> getAll();
        /// <summary>
        /// Get district by its' id
        /// </summary>
        /// <param name="districtId">the guid of a district</param>
        /// <returns>district object</returns>
        District fetchDistrictById(Guid districtId);
        /// <summary>
        /// Get district by it's name
        /// </summary>
        /// <param name="name">the name of a district</param>
        /// <returns>district object</returns>
        District fetchDistrictByName(string name);
        

    }
}
