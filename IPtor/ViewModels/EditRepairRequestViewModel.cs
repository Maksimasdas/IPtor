using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using System;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class EditRepairRequestViewModel : ViewModelBase
    {
        private readonly IRepairRequestService _requestService;
        private readonly RepairRequest _request;

        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        public string CarInfo { get; }

        public ICommand SaveCommand { get; }
        public Action CloseAction { get; set; }

        public EditRepairRequestViewModel(IRepairRequestService requestService, RepairRequest request)
        {
            _requestService = requestService;
            _request = request;

            CarInfo = $"{request.Car?.Name} - {request.Car?.Number}";
            Description = request.Description;

            SaveCommand = new RelayCommand(async _ => await SaveAsync(),
                _ => !string.IsNullOrWhiteSpace(Description));
        }

        private async Task SaveAsync()
        {
            _request.Description = Description;

            await _requestService.UpdateRequestAsync(_request);

            MessageBox.Show("Заявка обновлена!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            CloseAction?.Invoke();
        }
    }
}
