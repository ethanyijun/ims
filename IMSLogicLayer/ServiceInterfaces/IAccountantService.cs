
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSLogicLayer.Models;

namespace IMSLogicLayer.ServiceInterfaces
{
    public interface IAccountantService
    {
        /// <summary>
        /// Get all user in the system
        /// </summary>
        /// <returns>A list of user</returns>
        IEnumerable<User> getAllUser();
        /// <summary>
        /// Get all engineer in the system
        /// </summary>
        /// <returns>A list of engineer</returns>
        IEnumerable<User> getAllSiteEngineer();
        /// <summary>
        /// Get all manager in the system
        /// </summary>
        /// <returns>A list of manager</returns>
        IEnumerable<User> getAllManger();
        /// <summary>
        /// Get the user with it's id
        /// </summary>
        /// <param name="userId">The guid of an user</param>
        /// <returns>An user instance</returns>
        User getUserById(Guid userId);

        /// <summary>
        /// Get the current user instance
        /// </summary>
        /// <returns>The current user instance</returns>
        User getDetail();
        /// <summary>
        /// Change district of an user
        /// </summary>
        /// <param name="userId">The guid of the user</param>
        /// <param name="districtId">The guid of the district</param>
        /// <returns>True if success, false if fail</returns>
        bool changeDistrict(Guid userId, Guid district);
        /// <summary>
        /// Gets the report data for total cost by engineer report
        /// </summary>
        /// <returns>a list of reportrow to construct a report</returns>
        IEnumerable<ReportRow> printTotalCostByEngineer();
        /// <summary>
        /// Gets the report data for average cost by engineer report
        /// </summary>
        /// <returns>a list of reportrow to construct a report</returns>
        IEnumerable<ReportRow> printAverageCostByEngineer();
        /// <summary>
        /// Gets the report data for total cost by district report
        /// </summary>
        /// <returns>a list of reportrow to construct a report</returns>
        IEnumerable<ReportRow> printTotalCostByDistrict();
        /// <summary>
        /// Gets the report data for monthly cost by district report
        /// </summary>
        /// <returns>a list of reportrow to construct a report</returns>
        IEnumerable<ReportRow> printMonthlyCostByDistrict(Guid districtId);



    }
}
