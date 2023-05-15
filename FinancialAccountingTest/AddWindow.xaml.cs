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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        FinancialLogContext db = new FinancialLogContext();
        public AddWindow()
        {
            InitializeComponent();
            tbId.Text = GetLastID().ToString();
            tbDate.Text = DateTime.Now.ToString();
        }
        int GetLastID() 
        {
            var finLogs = db.FinancialLogs.ToList();
            int nextId = 1;
            if (finLogs.Count > 0)
                nextId = finLogs[finLogs.Count - 1].Id + 1;
            return nextId;
        }
        private void btnAddLog_Click(object sender, RoutedEventArgs e)
        {
            FLType type = FLType.Credit;
            try 
            {
                type = (FLType)int.Parse(tbType.Text); 
            } 
            catch { MessageBox.Show("Enum parser broken"); }
            var log = new FinancialLog() { Id = int.Parse(tbId.Text), Date = DateTime.Now, Description = tbDescription.Text, LogType = type, Value = double.Parse( tbValue.Text) };
            db.FinancialLogs.Add(log);
            db.SaveChanges();

            AdminWindow window = new AdminWindow();
            Close();
            window.ShowDialog();
        }
    }
}
