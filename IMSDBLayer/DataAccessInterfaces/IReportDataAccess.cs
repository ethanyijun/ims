using IMSDBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.DataAccessInterfaces
{
    public interface IReportDataAccess
    {
        /// <summary>
        /// Get report data from database
        /// </summary>
        /// <returns>A list of report data</returns>
        IEnumerable<ReportRow> totalCostByEngineer(int type,int state);
        /// <summary>
        /// Get report data from database
        /// </summary>
        /// <returns>A list of report data</returns>
        IEnumerable<ReportRow> averageCostByEngineer(int type, int state);
        /// <summary>
        /// Get report data from database
        /// </summary>
        /// <returns>A list of report data</returns>
        IEnumerable<ReportRow> costByDistrict(int state);
        /// <summary>
        /// Get report data from database
        /// </summary>
        /// <returns>A list of report data</returns>
        IEnumerable<ReportRow> monthlyCostForDistrict(Guid districtId,int state);

    }
}
