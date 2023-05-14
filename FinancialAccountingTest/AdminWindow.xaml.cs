using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinancialAccountingTest
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            List<FinancialLog> logs = new List<FinancialLog>() {
                new FinancialLog() { Id = 1, Date = DateTime.Now, Description = "Sample Text", LogType = FLType.Income, Value = 123 },
                new FinancialLog() { Id = 1, Date = DateTime.Now, Description = "Sample Text", LogType = FLType.Income, Value = 123 },
                new FinancialLog() { Id = 1, Date = DateTime.Now, Description = "Sample Text", LogType = FLType.Income, Value = 123 }
            };
            dgLogs.ItemsSource = logs;
        }
    }
}
