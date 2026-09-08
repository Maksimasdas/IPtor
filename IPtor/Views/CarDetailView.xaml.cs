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
    /// Логика взаимодействия для CarDetailView.xaml
    /// </summary>
    public partial class CarDetailView : Window
    {
        public CarDetailView(CarDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseAction = Close;  // ← Привязываем закрытие окна почему-то тут не работает
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UiHelper.TextBoxGotFocus(sender, e);
        }

        private void DefectsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CarDetailViewModel vm)
                vm.OpenDefectActCommand.Execute(null);
        }

        private void TOListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CarDetailViewModel vm)
                vm.OpenTOActCommand.Execute(null);
        }
    }
}
