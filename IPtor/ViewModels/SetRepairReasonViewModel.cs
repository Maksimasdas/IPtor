using IPtor.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class SetRepairReasonViewModel : ViewModelBase
    {
        private string _reason;
        public string Reason
        {
            get => _reason;
            set => Set(ref _reason, value);
        }

        public ICommand SaveCommand { get; }
        public Action CloseAction { get; set; }

        public SetRepairReasonViewModel()
        {
            SaveCommand = new RelayCommand(_ => Save(),
                _ => !string.IsNullOrWhiteSpace(Reason));
        }

        private void Save()
        {
            CloseAction?.Invoke();
        }
    }
}
