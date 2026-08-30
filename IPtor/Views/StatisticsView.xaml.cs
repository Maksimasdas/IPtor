using IPtor.ViewModels;
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
        private void UserControl_Loaded(object sender, RoutedEventArgs e) //событие которое срабатывает при загрузке ЮК и запускает загрузку данныхх к нему
        {
            if (DataContext is StatisticsViewModel vm)
            {
                vm.LoadDataCommand.Execute(null);
            }
        }
    }
}
