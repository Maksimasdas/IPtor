using System;
using System.Windows.Controls;

namespace IPtor.Helpers
{
    public static class UiHelper
    {
        public static void TextBoxGotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsReadOnly)
            {
                textBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    textBox.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
            }
        }
    }
}
