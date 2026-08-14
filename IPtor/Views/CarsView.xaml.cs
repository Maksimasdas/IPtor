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
    /// Логика взаимодействия для CarsView.xaml
    /// </summary>
    public partial class CarsView : UserControl
    {
        public CarsView()
        {
            InitializeComponent();
        }
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CarsViewModel vm && vm.SelectedCar != null)
            {
                vm.OpenCarDetailCommand.Execute(null);
            }
        }
    }
}
