using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAccountingTest
{
    public class MonthTuple
    {
        public DateTime InitialDate { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public MonthTuple()
        {
        }
        public MonthTuple(DateTime date, string name, double value)
        {
            InitialDate = date;
            Name = name;
            Value = value;
        }
    }
}
