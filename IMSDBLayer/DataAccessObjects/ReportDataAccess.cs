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

        public IEnumerable<ReportRow> averageCostByEngineer()
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from u in context.Users
                                join i in context.Interventions on u.Id equals i.CreatedBy
                                where u.Type == 1 && i.State == 2
                                select new { Name = u.Name, Cost = i.Costs, Hour = i.Hours } into x
                                group x by x.Name into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Name, Costs = y.Average(i => i.Cost), Hours = y.Average(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty);

            }
        }

        public IEnumerable<ReportRow> costByDistrict()
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from c in context.Clients
                                join d in context.Districts on c.DistrictId equals d.Id
                                join i in context.Interventions on c.Id equals i.ClientId
                                where i.State == 2
                                select new { Name = d.Name, Cost = i.Costs, Hour = i.Hours } into x
                                group x by x.Name into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Name, Costs = y.Sum(i => i.Cost), Hours = y.Sum(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty);

            }
        }

        public IEnumerable<ReportRow> monthlyCostForDistrict(Guid districtId)
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from c in context.Clients
                                join i in context.Interventions on c.Id equals i.ClientId
                                where i.State == 2 && c.DistrictId == districtId
                                select new { Date = i.DateFinish, Cost = i.Costs, Hour = i.Hours } into x

                                group x by x.Date into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Date.Value.ToString("y"), Costs = y.Sum(i => i.Cost), Hours = y.Sum(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty);

            }
        }

        public IEnumerable<ReportRow> totalCostByEngineer()
        {
            using (IMSEntities context = new IMSEntities())
            {
                var reportRow = from u in context.Users
                                join i in context.Interventions on u.Id equals i.CreatedBy
                                where u.Type == 1 && i.State == 2
                                select new { Name = u.Name, Cost = i.Costs, Hour = i.Hours } into x
                                group x by x.Name into y
                                select new ReportRow() { FirstProperty = y.FirstOrDefault().Name, Costs = y.Sum(i => i.Cost), Hours = y.Sum(s => s.Hour) };

                return reportRow.OrderBy(r => r.FirstProperty);

            }
        }
    }
}
