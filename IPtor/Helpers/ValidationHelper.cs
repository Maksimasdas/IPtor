using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace IPtor.Helpers
{
    public static class ValidationHelper
    {
        // Проверяет ввод только цифры, точка и запятая.
        public static void ValidateFloatInput(object sender, TextCompositionEventArgs e)
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

        // Проверяет ввод только целые числа.
        public static void ValidateIntInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}
