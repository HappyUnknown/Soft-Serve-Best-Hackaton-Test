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
            try
            {
                var log = db.FinancialLogs.ToList();
                var periodics = new List<FinancialLog>();
                var months = new List<MonthTuple>();
                for (int i = 0; i < log.Count; i++)
                {
                    string title = $"{(Months)log[i].Date.Month}, {log[i].Date.Year}";
                    int tindex = TitleIndex(title, ref months);
                    if (tindex == -1)
                    {
                        months.Add(new MonthTuple(log[i].Date, title, 0));
                        tindex = months.Count - 1;
                    }
                    switch (log[i].LogType)
                    {
                        case FLType.Income:
                            months[tindex].Value += log[i].Value;
                            break;
                        case FLType.Spending:
                            months[tindex].Value -= log[i].Value;
                            break;
                        case FLType.Credit:
                            periodics.Add(new FinancialLog() { Id = log[i].Id, Date = log[i].Date, Description = log[i].Description, LogType = log[i].LogType, Value = -log[i].Value });
                            break;
                        case FLType.Deposit:
                            periodics.Add(log[i]);
                            break;
                        default:
                            MessageBox.Show($"Unsigned type on ID-{log[i].Id}");
                            break;
                    }

                }

                for (int i = 0; i < months.Count; i++)
                    for (int j = 0; j < periodics.Count; j++)
                        if (periodics[j].Date.Month <= months[i].InitialDate.Month && periodics[j].Date.Year <= months[i].InitialDate.Year)
                            months[i].Value += periodics[j].Value;

                ReportWindow report = new ReportWindow(months);
                //Close();
                report.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                //MessageBox.Show($"Success removing");
            }
            catch(NullReferenceException) 
            {
                MessageBox.Show("You cannot remove an element ommiting its selection.");
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
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("You cannot edit an element ommiting its selection.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"You cannot edit an element ommiting its selection. {ex.Message}");
            }
        }
    }
}
