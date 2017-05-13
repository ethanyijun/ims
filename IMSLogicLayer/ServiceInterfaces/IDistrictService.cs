using IMSLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.ServiceInterfaces
{
    public interface IDistrictService
    {
        /// <summary>
        /// Get all district 
        /// </summary>
        /// <returns>A list of districts</returns>
        IEnumerable<District> GetAllDistrict();
        /// <summary>
        /// Get a district from it's id
        /// </summary>
        /// <param name="Id">The guid of a district</param>
        /// <returns>A district instance</returns>
        District GetDistrictById(Guid Id);
        /// <summary>
        /// Get a district from it's name
        /// </summary>
        /// <param name="name">name of a district</param>
        /// <returns>A district instance</returns>
        District GetDistrictByName(string name);

    }
}
