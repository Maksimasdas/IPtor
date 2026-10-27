using IPtor.ViewModels;
using LiveChartsCore.Measure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IPtor.Views
{
    /// <summary>
    /// Логика взаимодействия для StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        public StatisticsView()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //отключаем тултип для графика ибо он лагучий я запарился
            RepairChart.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Hidden;

            if (DataContext is StatisticsViewModel vm)
            {
                vm.LoadDataCommand.Execute(null);
            }
        }

    }
}
