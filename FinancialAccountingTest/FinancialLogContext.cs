using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAccountingTest
{
    class FinancialLogContext : DbContext
    {
        public FinancialLogContext() : base("financeConnection")
        {
            
        }
        public DbSet<FinancialLog> FinancialLogs { get; set; }
    }
}
