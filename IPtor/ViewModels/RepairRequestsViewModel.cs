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

        private List<RepairRequest> _allRequests;

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

        private Car _selectedCarFilter;
        public Car SelectedCarFilter
        {
            get => _selectedCarFilter;
            set
            {
                if (Set(ref _selectedCarFilter, value))
                {
                    FilterRequests();
                }
            }
        }

        private string _carSearchText;
        public string CarSearchText
        {
            get => _carSearchText;
            set
            {
                if (Set(ref _carSearchText, value))
                {
                    FilterCarsForCombo();
                }
            }
        }
        private List<Car> _allCarsList;

        public ICommand LoadRequestsCommand { get; }
        public ICommand DeleteRequestCommand { get; }
        public ICommand AcceptForRepairCommand { get; }
        public ICommand EditRequestCommand { get; }

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

            LoadRequestsCommand = new RelayCommand(async _ =>
            {
                await LoadDataAsync();        // Сначала загружаем машины и заявки
                CarSearchText = string.Empty;  // Потом сбрасываем поиск
                SelectedCarFilter = null;      // И фильтр
            });
            DeleteRequestCommand = new RelayCommand(async _ => await DeleteRequestAsync(),
                _ => SelectedRequest != null);
            AcceptForRepairCommand = new RelayCommand(_ => AcceptForRepair(),
                _ => SelectedRequest != null);
            EditRequestCommand = new RelayCommand(_ => EditRequest(),
                _ => SelectedRequest != null);
        }

        private async Task LoadDataAsync()
        {
            await LoadCarsAsync();
            await LoadRequestsAsync();
        }

        private async Task LoadCarsAsync()
        {
            var cars = await _carService.GetAllCarsAsync();
            _allCarsList = cars.ToList();
            Cars = new ObservableCollection<Car>(_allCarsList);
            //Cars = new ObservableCollection<Car>(cars);
            //Cars = new ObservableCollection<Car>(_allCarsList);
        }

        private async Task LoadRequestsAsync()
        {
            var requests = (await _requestService.GetAllRequestsAsync()).ToList();

            foreach (var request in requests)
            {
                request.Car = Cars.FirstOrDefault(c => c.CarId == request.CarId);
            }

            _allRequests = requests;
            FilterRequests();
        }

        private void FilterRequests()
        {
            if (SelectedCarFilter == null)
            {
                Requests = new ObservableCollection<RepairRequest>(_allRequests);
            }
            else
            {
                var filtered = _allRequests
                    .Where(r => r.CarId == SelectedCarFilter.CarId)
                    .ToList();
                Requests = new ObservableCollection<RepairRequest>(filtered);
            }
        }

        private async Task DeleteRequestAsync()
        {
            if (SelectedRequest == null) return;

            await _requestService.DeleteRequestAsync(SelectedRequest.RepairRequestId);
            _allRequests.Remove(SelectedRequest);
            FilterRequests();
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

        private async void EditRequest()
        {
            if (SelectedRequest == null) return;

            var editVM = new EditRepairRequestViewModel(_requestService, SelectedRequest);
            var window = new EditRepairRequestView(editVM);
            window.ShowDialog();

            await LoadRequestsAsync();
        }

        private void FilterCarsForCombo()
        {
            if (_allCarsList == null) return;

            if (string.IsNullOrWhiteSpace(CarSearchText))
            {
                Cars = new ObservableCollection<Car>(_allCarsList);
                return;
            }

            var filtered = _allCarsList.Where(car =>
                car.Number.Contains(CarSearchText, StringComparison.OrdinalIgnoreCase) ||
                car.Name.Contains(CarSearchText, StringComparison.OrdinalIgnoreCase) ||
                car.Model.Contains(CarSearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            Cars = new ObservableCollection<Car>(filtered);
        }
    }
}