using IPtor.Helpers;
using IPtor.ViewModels;
using System.Windows;
using System.Windows.Input;

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
            ValidationHelper.ValidateFloatInput(sender, e);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UiHelper.TextBoxGotFocus(sender, e);
        }
    }
}
