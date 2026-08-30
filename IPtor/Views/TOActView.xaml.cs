using IPtor.Helpers;
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
            UiHelper.TextBoxGotFocus(sender, e);
        }
        private void FuelValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            ValidationHelper.ValidateFloatInput(sender, e);
        }
    }
}