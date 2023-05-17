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
            lblId.Content = GetLastID().ToString();
            dpDate.Text = DateTime.Now.ToString();
            cmbType.ItemsSource = Enum.GetValues(typeof(FLType));
            cmbType.SelectedIndex = 0;
        }
        int GetLastID()
        {
            var finLogs = db.FinancialLogs.ToList();
            int nextId = 1;
            if (finLogs.Count > 0)
                nextId = finLogs[finLogs.Count - 1].Id + 1;
            return nextId;
        }
        void GetBack()
        {
            AdminWindow window = new AdminWindow();
            Close();
            window.ShowDialog();
        }

        private void btnAddLog_Click(object sender, RoutedEventArgs e)
        {
            FLType type = FLType.Credit;
            try
            {
                type = (FLType)(cmbType.SelectedIndex + 1);
            }
            catch (Exception ex) { MessageBox.Show($"Enum parser broken: {ex.Message}"); }

            double costValue;
            if (double.TryParse(tbValue.Text, out costValue))
            {
                if (tbDescription.Text.Trim(' ').Length == 0)
                    if (MessageBox.Show("You may need some caption for your accounting. Negotiate?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        return;
             
                var log = new FinancialLog() { Id = int.Parse(lblId.Content.ToString()), Date = dpDate.DisplayDate, Description = tbDescription.Text, LogType = type, Value = costValue };
                db.FinancialLogs.Add(log);
                db.SaveChanges();

                GetBack();
            }
            else
            {
                MessageBox.Show("It does not seem you are intending to add non-numeric prive log.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GetBack();
        }
    }
}
