using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class RepairRequestsViewModel : ViewModelBase
    {
        private readonly IRepairRequestService _requestService;
        private readonly ICarService _carService;
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;

        private List<Car> _allCars;

        private ObservableCollection<RepairRequest> _requests;
        public ObservableCollection<RepairRequest> Requests
        {
            get => _requests;
            set => Set(ref _requests, value);
        }

        private RepairRequest _selectedRequest;
        public RepairRequest SelectedRequest
        {
            get => _selectedRequest;
            set => Set(ref _selectedRequest, value);
        }

        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set => Set(ref _cars, value);
        }

        private Car _selectedCar;
        public Car SelectedCar
        {
            get => _selectedCar;
            set => Set(ref _selectedCar, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (Set(ref _searchText, value))
                {
                    FilterCars();
                }
            }
        }

        public ICommand LoadRequestsCommand { get; }
        public ICommand AddRequestCommand { get; }
        public ICommand DeleteRequestCommand { get; }
        public ICommand AcceptForRepairCommand { get; }

        public RepairRequestsViewModel(
    IRepairRequestService requestService,
    ICarService carService,
    IPersonService personService,
    IReportService reportService)
        {
            _requestService = requestService;
            _carService = carService;
            _personService = personService;
            _reportService = reportService;

            // Команда обновления загружает всё последовательно
            LoadRequestsCommand = new RelayCommand(async _ => await LoadDataAsync());

            AddRequestCommand = new RelayCommand(async _ => await AddRequestAsync(),
                _ => SelectedCar != null && !string.IsNullOrWhiteSpace(Description));
            DeleteRequestCommand = new RelayCommand(async _ => await DeleteRequestAsync(),
                _ => SelectedRequest != null);
            AcceptForRepairCommand = new RelayCommand(_ => AcceptForRepair(),
                _ => SelectedRequest != null);

            // БЕЗ автозагрузки
        }


        private async Task LoadDataAsync()
        {
            try
            {
                await LoadCarsAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка машин: {ex.Message}");
            }

            try
            {
                await LoadRequestsAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка заявок: {ex.Message}");
            }
        }

        private async Task LoadCarsAsync()
        {
            var cars = await _carService.GetAllCarsAsync();
            _allCars = cars.ToList();
            Cars = new ObservableCollection<Car>(_allCars);
        }

        private async Task LoadRequestsAsync()
        {
            var requests = (await _requestService.GetAllRequestsAsync()).ToList();

            foreach (var request in requests)
            {
                request.Car = Cars.FirstOrDefault(c => c.CarId == request.CarId);
            }

            Requests = new ObservableCollection<RepairRequest>(requests);
        }

        private async Task AddRequestAsync()
        {
            var request = new RepairRequest
            {
                CarId = SelectedCar.CarId,
                Description = Description,
                RequestDate = DateTime.Now,
                Status = "Ожидает ремонта"
            };

            await _requestService.AddRequestAsync(request);

            Description = string.Empty;
            await LoadRequestsAsync();
        }

        private async Task DeleteRequestAsync()
        {
            if (SelectedRequest == null) return;

            await _requestService.DeleteRequestAsync(SelectedRequest.RepairRequestId);
            Requests.Remove(SelectedRequest);
        }

        private void AcceptForRepair()
        {
            if (SelectedRequest == null) return;

            var car = Cars.FirstOrDefault(c => c.CarId == SelectedRequest.CarId);
            if (car == null) return;

            var actVM = new TransferActViewModel(_carService, _personService, _reportService, car);
            var window = new TransferActView(actVM);
            window.ShowDialog();

            _ = DeleteRequestAsync();
        }
        private void FilterCars()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Cars = new ObservableCollection<Car>(_allCars);
                return;
            }

            var filtered = _allCars.Where(car =>
                car.Number.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                car.Model.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                car.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            Cars = new ObservableCollection<Car>(filtered);
        }
    }
}