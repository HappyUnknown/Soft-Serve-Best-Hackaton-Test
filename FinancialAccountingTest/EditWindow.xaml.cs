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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        FinancialLogContext db = new FinancialLogContext();
        public EditWindow()
        {
            InitializeComponent();
        }
        public EditWindow(FinancialLog log)
        {
            InitializeComponent();

            cmbType.ItemsSource = Enum.GetValues(typeof(FLType));

            lblId.Content = log.Id.ToString();
            tbDescription.Text = log.Description;
            tbValue.Text = log.Value.ToString();
            dpDate.SelectedDate = log.Date;
            cmbType.SelectedIndex = (int)log.LogType - 1;
        }

        private void btnEditLog_Click(object sender, RoutedEventArgs e)
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

                int id = int.Parse(lblId.Content.ToString());
                var logInList = db.FinancialLogs.ToList().Where(x => x.Id == id).FirstOrDefault();
                logInList.LogType = (FLType)(cmbType.SelectedIndex + 1);
                logInList.Value =double.Parse(tbValue.Text);
                logInList.Description = tbDescription.Text;
                logInList.Date = dpDate.DisplayDate;
                db.Entry(logInList).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                AdminWindow window = new AdminWindow();
                Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("It does not seem you are intending to add non-numeric prive log.");
            }
        }
    }
}