using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAccountingTest
{
    class FinancialLog
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public FLType LogType { get; set; }
    }
}
