using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDBLayer.Models;
using IMSDBLayer.DataAccessInterfaces;

namespace IMSDBLayer.DataAccessObjects
{
    public class ReportDataAccess : IReportDataAccess
    {
        public ReportDataAccess() { }

        public IEnumerable<ReportRow> averageCostByEngineer(int type, int state)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from u in context.Users
                                join i in context.Interventions on u.Id equals i.CreatedBy
                                where u.Type == type && i.State == state
                                select new { Name = u.Name, Cost = i.Costs, Hour = i.Hours } into x
                                group x by x.Name into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Name, Costs = y.Average(i => i.Cost), Hours = y.Average(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty).ToList();

            }
        }

        public IEnumerable<ReportRow> costByDistrict(int state)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from c in context.Clients
                                join d in context.Districts on c.DistrictId equals d.Id
                                join i in context.Interventions on c.Id equals i.ClientId
                                where i.State == state
                                select new { Name = d.Name, Cost = i.Costs, Hour = i.Hours } into x
                                group x by x.Name into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Name, Costs = y.Sum(i => i.Cost), Hours = y.Sum(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty).ToList();

            }
        }

        public IEnumerable<ReportRow> monthlyCostForDistrict(Guid districtId, int state)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from c in context.Clients
                                join i in context.Interventions on c.Id equals i.ClientId
                                where i.State == state && c.DistrictId == districtId
                                select new { Date = i.DateFinish, Cost = i.Costs, Hour = i.Hours } into x

                                group x by x.Date into y
                                select new ReportRow() { FirstProperty = System.Data.Entity.SqlServer.SqlFunctions.DateName("month",y.FirstOrDefault().Date.Value)+"/" + System.Data.Entity.SqlServer.SqlFunctions.DateName("year", y.FirstOrDefault().Date.Value), Costs = y.Sum(i => i.Cost), Hours = y.Sum(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty).ToList();

            }
        }

        public IEnumerable<ReportRow> totalCostByEngineer(int type,int state)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from u in context.Users
                                join i in context.Interventions on u.Id equals i.CreatedBy
                                where u.Type == type && i.State == state
                                select new { Name = u.Name, Cost = i.Costs, Hour = i.Hours } into x
                                group x by x.Name into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Name, Costs = y.Sum(i => i.Cost), Hours = y.Sum(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty).ToList();

            }
        }
    }
}
