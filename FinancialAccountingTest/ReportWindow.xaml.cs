﻿using System;
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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            pltReport.Plot.AddLine(0, 0, 10, 10);
            pltReport.Plot.AddLine(10, 10, 20, 0);
            pltReport.Plot.AddLine(20, 0, 30, 10);
        }
        public ReportWindow(List<MonthTuple> months)
        {
            InitializeComponent();

            List<double> values = new List<double>();
            for (int i = 0; i < months.Count; i++)
                values.Add(months[i].Value);
            List<double> positions = new List<double>();
            for (int i = 0; i < months.Count; i++)
                positions.Add(i);
            try
            {
                List<string> labels = new List<string>();
                for (int i = 0; i < months.Count; i++)
                    labels.Add(months[i].Name);
                var bar = pltReport.Plot.AddBar(values.ToArray(), positions.ToArray());
                bar.ShowValuesAboveBars = true;

                System.Windows.Media.Color mediacolor = (FindResource("ForegroundColor") as SolidColorBrush).Color; // your color
                var drawingcolor = System.Drawing.Color.FromArgb(
                    mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);

                bar.FillColorNegative = drawingcolor;
                bar.FillColor = drawingcolor;
                pltReport.Plot.XTicks(positions.ToArray(), labels.ToArray());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //pltReport.Plot.SetAxisLimits(yMin: 0);
        }
    }
}
