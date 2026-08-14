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
    /// Логика взаимодействия для TransferActView.xaml
    /// </summary>
    public partial class TransferActView : Window
    {
        public TransferActView(TransferActViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FuelValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != ',' && c != '.')
                {
                    e.Handled = true;
                    return;
                }
            }

            if (textBox != null)
            {
                string currentText = textBox.Text;
                if ((e.Text.Contains(",") || e.Text.Contains(".")) &&
                    (currentText.Contains(",") || currentText.Contains(".")))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsReadOnly)
            {
                // Небольшая задержка, чтобы выделение сработало корректно
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    textBox.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
            }
        }
    }
}
