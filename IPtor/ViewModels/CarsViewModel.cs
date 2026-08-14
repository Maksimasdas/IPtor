using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using IPtor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class CarsViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;

        // ===== КОЛЛЕКЦИЯ МАШИН =====
        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set => Set(ref _cars, value);
        }
        // ===== ВЫБРАННАЯ МАШИНА =====
        private Car _selectedCar;
        public Car SelectedCar
        {
            get => _selectedCar;
            set => Set(ref _selectedCar, value);
        }

        // ===== ФИЛЬТР ПО СЛУЖБЕ =====
        private string _selectedServiceFilter = "Все службы";
        public string SelectedServiceFilter
        {
            get => _selectedServiceFilter;
            set
            {
                if (Set(ref _selectedServiceFilter, value))
                {
                    _ = LoadCarsAsync();
                }
            }
        }

        // ===== СВОЙСТВО ЗАГРУЗКИ =====
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }
        // ===== КОМАНДЫ =====
        public ICommand LoadCarsCommand { get; }
        public ICommand DeleteCarCommand { get; }
        public ICommand AddCarCommand { get; }
        public ICommand OpenCarDetailCommand { get; }
        public ICommand CreateTransferActCommand { get; }
        public ICommand CreateTOActCommand { get; }

        // ===== СПИСОК СЛУЖБ ДЛЯ ФИЛЬТРА =====
        public ObservableCollection<string> ServiceTypes { get; }

        // ===== КОНСТРУКТОР =====
        // ICarService внедряется через DI
        public CarsViewModel(ICarService carService, IPersonService personService, IReportService reportService)
        {
            _carService = carService;
            _personService = personService;
            _reportService = reportService;

            ServiceTypes = new ObservableCollection<string> { "Все службы" };
            foreach (var service in System.Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            // Привязываем команды к методам
            LoadCarsCommand = new RelayCommand(async _ => await LoadCarsAsync());
            DeleteCarCommand = new RelayCommand(async _ => await DeleteCarAsync(), _ => SelectedCar != null);
            AddCarCommand = new RelayCommand(_ => OpenAddCarWindow());
            OpenCarDetailCommand = new RelayCommand(_ => OpenCarDetail(), _ => SelectedCar != null);
            CreateTransferActCommand = new RelayCommand(_ => OpenTransferActWindow(), _ => SelectedCar != null);
            CreateTOActCommand = new RelayCommand(_ => OpenTOActWindow(), _ => SelectedCar != null);

            // При старте сразу загружаем машины
            _ = LoadCarsAsync();
        }

        // ===== МЕТОДЫ =====
        private void OpenAddCarWindow()
        {
            var addCarVM = new AddCarViewModel(_carService);
            var window = new AddCarWindow(addCarVM);
            window.ShowDialog();

            // После закрытия окна — обновляем список
            _ = LoadCarsAsync();
        }
        private async Task LoadCarsAsync()
        {
            IsLoading = true;
            try
            {
                IEnumerable<Car> cars;

                if (SelectedServiceFilter == "Все службы")
                {
                    cars = await _carService.GetCarsWithDetailsAsync();
                }
                else
                {
                    var service = (ServiceType)System.Enum.Parse(typeof(ServiceType), SelectedServiceFilter);
                    cars = await _carService.GetCarsByServiceAsync(service);
                }

                Cars = new ObservableCollection<Car>(cars);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task DeleteCarAsync()
        {
            if (SelectedCar == null) return;

            await _carService.DeleteCarAsync(SelectedCar.CarId);

            // Удаляем из коллекции на экране
            Cars.Remove(SelectedCar);
        }
        private void OpenCarDetail()
        {
            if(SelectedCar == null) return;

            var detailVM = new CarDetailViewModel(_carService, SelectedCar.CarId);
            var window = new CarDetailView(detailVM);
            window.ShowDialog();
        }
        //создать акт передачи (открывается окно создания акта)
        private void OpenTransferActWindow()
        {
            if (SelectedCar == null) return;

            var actVM = new TransferActViewModel(_carService, _personService, _reportService, SelectedCar);
            var window = new TransferActView(actVM);
            window.ShowDialog();
        }
        
        //создание акта тех состояния
        private void OpenTOActWindow()
        {
            if (SelectedCar == null) return;

            var actVM = new TOActViewModel(_personService, _reportService, SelectedCar);
            var window = new TOActView(actVM);
            window.ShowDialog();
        }
    }
}
