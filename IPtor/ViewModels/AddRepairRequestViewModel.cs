using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using System;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class AddRepairRequestViewModel : ViewModelBase
    {
        private readonly IRepairRequestService _requestService;
        private readonly Car _car;
        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref  _description, value);
        }

        public string CarInfo { get; set;  }

        public ICommand SaveCommand { get; }
        public Action CloseAction { get; set; }

        public AddRepairRequestViewModel(IRepairRequestService requestService, Car car)
        {
            _requestService = requestService;
            _car = car;

            CarInfo = $"{car.Name} - {car.Number}";

            SaveCommand = new RelayCommand(async _ => await SaveAsync(),
                _ => !string.IsNullOrWhiteSpace(Description));
        }
        private async Task SaveAsync()
        {
            var request = new RepairRequest
            {
                CarId = _car.CarId,
                Description = Description,
                RequestDate = DateTime.Now,
                Status = "Ожидает ремонта"
            };

            await _requestService.AddRequestAsync(request);

            MessageBox.Show("Заявка создана!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            CloseAction?.Invoke();
        }

    }
}
