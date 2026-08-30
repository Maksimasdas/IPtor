using IPtor.Helpers;
using IPtor.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IPtor.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonsView.xaml
    /// </summary>
    public partial class PersonsView : UserControl
    {
        public PersonsView()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is PersonsViewModel vm && vm.SelectedPerson != null)
            {
                vm.OpenPersonDetailCommand.Execute(null);
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UiHelper.TextBoxGotFocus(sender, e);
        }
    }
}
