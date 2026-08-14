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
    }
}
