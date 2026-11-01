using IPtor.Helpers;
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
    /// Логика взаимодействия для EquipmentView.xaml
    /// </summary>
    public partial class EquipmentView : UserControl
    {
        public EquipmentView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UiHelper.TextBoxGotFocus(sender, e);
        }

        // Двойной клик по оборудованию — открыть детали
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EquipmentViewModel vm && vm.SelectedEquipment != null)
            {
                vm.OpenEquipmentDetailCommand.Execute(null);
            }
        }
    }
}
