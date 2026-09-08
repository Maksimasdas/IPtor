using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using IPtor.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class CarsViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;
        private readonly IRepairRequestService _repairRequestService;

        private List<Car> _allCars;

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
                    //_ = LoadCarsAsync();
                    ApplyFilters();
                }
            }
        }

        //поиск по номеру
        private string _searchNumber;
        public string SearchNumber
        {
            get => _searchNumber;
            set => Set(ref _searchNumber, value);
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
        public ICommand CreateRepairRequestCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        // ===== СПИСОК СЛУЖБ ДЛЯ ФИЛЬТРА =====
        public ObservableCollection<string> ServiceTypes { get; }

        // ===== КОНСТРУКТОР =====
        // ICarService внедряется через DI
        public CarsViewModel(ICarService carService, IPersonService personService, IReportService reportService, IRepairRequestService repairRequestService)
        {
            _carService = carService;
            _personService = personService;
            _reportService = reportService;
            _repairRequestService = repairRequestService;

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
            CreateRepairRequestCommand = new RelayCommand(_ => OpenRepairRequestWindow(), _ => SelectedCar != null);
            SearchCommand = new RelayCommand(_ => ApplyFilters());
            ClearSearchCommand = new RelayCommand(_ => ClearSearch());

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
        //private async Task LoadCarsAsync()
        //{
        //    IsLoading = true;
        //    try
        //    {
        //        IEnumerable<Car> cars;

        //        if (SelectedServiceFilter == "Все службы")
        //        {
        //            cars = await _carService.GetCarsWithDetailsAsync();
        //        }
        //        else
        //        {
        //            var service = (ServiceType)System.Enum.Parse(typeof(ServiceType), SelectedServiceFilter);
        //            cars = await _carService.GetCarsByServiceAsync(service);
        //        }

        //        Cars = new ObservableCollection<Car>(cars);
        //    }
        //    finally
        //    {
        //        IsLoading = false;
        //    }
        //}
        private async Task LoadCarsAsync()
        {
            IsLoading = true;
            try
            {
                var cars = await _carService.GetCarsWithDetailsAsync();
                _allCars = cars.ToList();
                ApplyFilters();
            }
            finally
            {
                IsLoading = false;
            }
        }
        private void ApplyFilters()
        {
            if (_allCars == null) return;

            IEnumerable<Car> filtered = _allCars;

            // Фильтр по службе
            if (SelectedServiceFilter != "Все службы")
            {
                var service = (ServiceType)System.Enum.Parse(typeof(ServiceType), SelectedServiceFilter);
                filtered = filtered.Where(c => c.Service == service);
            }

            // Фильтр по номеру
            if (!string.IsNullOrWhiteSpace(SearchNumber))
            {
                filtered = filtered.Where(c =>
                    c.Number.Contains(SearchNumber, StringComparison.OrdinalIgnoreCase) ||
                    c.Name.Contains(SearchNumber, StringComparison.OrdinalIgnoreCase) ||
                    c.Model.Contains(SearchNumber, StringComparison.OrdinalIgnoreCase));
            }

            Cars = new ObservableCollection<Car>(filtered);
        }
        private void ClearSearch()
        {
            SearchNumber = string.Empty;
            //ApplyFilters();
            _ = LoadCarsAsync();
        }
        private async Task DeleteCarAsync()
        {
            if (SelectedCar == null) return;

            MessageBoxResult result = MessageBox.Show(
                   $"Вы действительно хотите удалить автомобиль {_selectedCar.Model} ({_selectedCar.Number})?",
                   "Подтверждение удаления",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await _carService.DeleteCarAsync(SelectedCar.CarId);

                await LoadCarsAsync();
                //Cars.Remove(SelectedCar);
                //_allCars.Remove(SelectedCar);
            }
        }
        private void OpenCarDetail()
        {
            if (SelectedCar == null) return;

            var detailVM = new CarDetailViewModel(_carService, _reportService, SelectedCar.CarId);
            var window = new CarDetailView(detailVM);
            window.ShowDialog();

            _ = LoadCarsAsync();//добавил для обновления машин после редактирования
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

            var actVM = new TOActViewModel(_personService, _reportService, _carService, SelectedCar);
            var window = new TOActView(actVM);
            window.ShowDialog();
        }

        //Открытие окна добавления заявок на ремонт
        private void OpenRepairRequestWindow()
        {
            //System.Windows.MessageBox.Show("Команда вызвана!");//для проверки и выявления глюка
            if (SelectedCar == null) return;

            var requestVM = new AddRepairRequestViewModel(_repairRequestService, SelectedCar);
            var window = new AddRepairRequestView(requestVM);
            window.ShowDialog();
        }
    }
}
