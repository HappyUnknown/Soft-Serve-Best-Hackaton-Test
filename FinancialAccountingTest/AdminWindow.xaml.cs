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
        FinancialLogContext db = new FinancialLogContext();
        public AdminWindow()
        {
            InitializeComponent();
            try
            {
                dgLogs.ItemsSource = db.FinancialLogs.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnOpenAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            Close();
            addWindow.ShowDialog();
        }

        int TitleIndex(string month, ref List<MonthTuple> tuple)
        {
            for (int i = 0; i < tuple.Count; i++)
                if (tuple[i].Name == month)
                    return i;
            return -1;
        }

        private void btnGetReport_Click(object sender, RoutedEventArgs e)
        {
            var log = db.FinancialLogs.ToList();
            var months = new List<MonthTuple>();
            for (int i = 0; i < log.Count; i++)
            {
                string title = $"Month {log[i].Date.Month}, {log[i].Date.Year}";
                int tindex = TitleIndex(title, ref months);
                if (tindex == -1)
                    months.Add(new MonthTuple(title, log[i].Value));
                else
                    months[tindex].Value += log[i].Value;
            }

            ReportWindow report = new ReportWindow(months);
            //Close();
            report.ShowDialog();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var log = dgLogs.SelectedItem as FinancialLog;
                var listLog = db.FinancialLogs.ToList().Where(x => x.Id == log.Id).FirstOrDefault();
                db.FinancialLogs.Remove(listLog);
                db.SaveChanges();
                dgLogs.ItemsSource = db.FinancialLogs.ToList();
                MessageBox.Show($"Success removing");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var log = new FinancialLogContext().FinancialLogs.ToList()[dgLogs.SelectedIndex];
                EditWindow editWindow = new EditWindow(log);
                Close();
                editWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on index: {dgLogs.SelectedIndex} => {ex.Message}");
            }
        }
    }
}
