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
    /// Логика взаимодействия для RepairsView.xaml
    /// </summary>
    public partial class RepairsView : UserControl
    {
        public RepairsView()
        {
            InitializeComponent();
        }
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is RepairsViewModel vm && vm.SelectedRepair != null)
            {
                vm.OpenRepairActCommand.Execute(null);
            }
        }
    }
}
