using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAccountingTest
{
    public class MonthTuple
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public MonthTuple()
        {
        }
        public MonthTuple(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
