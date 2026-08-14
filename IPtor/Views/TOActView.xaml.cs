using IPtor.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IPtor.Views
{
    public partial class TOActView : Window
    {
        public TOActView(TOActViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsReadOnly)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    textBox.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
            }
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
    }
}