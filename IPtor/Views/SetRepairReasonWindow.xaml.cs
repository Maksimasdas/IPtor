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
using System.Windows.Shapes;

namespace IPtor.Views
{
    /// <summary>
    /// Логика взаимодействия для SetRepairReasonWindow.xaml
    /// </summary>
    public partial class SetRepairReasonWindow : Window
    {
        public string Reason { get; private set; }
        public SetRepairReasonWindow()
        {
            InitializeComponent();
            var vm = new SetRepairReasonViewModel();
            vm.CloseAction = () =>
            {
                Reason = vm.Reason;
                DialogResult = true;
            };
            DataContext = vm;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UiHelper.TextBoxGotFocus(sender, e);
        }
    }
}
