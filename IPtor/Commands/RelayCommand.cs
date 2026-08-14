using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IPtor.Commands
{
    public class RelayCommand : ICommand //класс стандартный, изменений без особой необходиомсти не нужно
    {
        private Func<object, bool> canExecute;
        private Action<object> executeMethod;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> executeMethod)
        {
            this.executeMethod = executeMethod;
        }
        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecute)
        {
            this.canExecute = canExecute;
            this.executeMethod = executeMethod;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            executeMethod(parameter);
        }
    }
}
