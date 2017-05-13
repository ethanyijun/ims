using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDBLayer.Models
{
    public partial class ReportRow
    {
        private string firstProperty;

        private decimal? costs;

        private decimal? hours;
        public ReportRow() { }

        public string FirstProperty
        {
            get
            {
                return firstProperty;
            }

            set
            {
                firstProperty = value;
            }
        }

        public decimal? Costs
        {
            get
            {
                return costs;
            }

            set
            {
                costs = value;
            }
        }

        public decimal? Hours
        {
            get
            {
                return hours;
            }

            set
            {
                hours = value;
            }
        }
    }
}
