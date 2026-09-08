using Azure.Core.Pipeline;
using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class AddCarViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        public Action CloseAction { get; set; } //для закрытия окна после добавлнеия машины
        // ===== ПОЛЯ МАШИНЫ =====
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _model;
        public string Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        private string _number;
        public string Number
        {
            get => _number;
            set => Set(ref _number, value);
        }

        private string _className;
        public string ClassName
        {
            get => _className;
            set => Set(ref _className, value);
        }

        private string _selectedService;
        public string SelectedService
        {
            get => _selectedService;
            set => Set(ref _selectedService, value);
        }

        private string _vin;
        public string Vin
        {
            get => _vin;
            set => Set(ref _vin, value);
        }

        private string _certificate;
        public string Certificate
        {
            get => _certificate;
            set => Set(ref _certificate, value);
        }

        private string _engineNumber;
        public string EngineNumber
        {
            get => _engineNumber;
            set => Set(ref _engineNumber, value);
        }

        private int _formular;
        public int Formular
        {
            get => _formular;
            set => Set(ref _formular, value);
        }

        private DateTime _formularDate = DateTime.Now;
        public DateTime FormularDate
        {
            get => _formularDate;
            set => Set(ref _formularDate, value);
        }

        private int _year = DateTime.Now.Year;
        public int Year
        {
            get => _year;
            set => Set(ref _year, value);
        }

        private string _inventoryNumber;
        public string InventoryNumber
        {
            get => _inventoryNumber;
            set => Set(ref _inventoryNumber, value);
        }

        // ===== КОЛЛЕКЦИИ =====
        public ObservableCollection<Battery> Batteries { get; set; }
        public ObservableCollection<Tire> Tires { get; set; }

        // ===== СПИСОК СЛУЖБ =====
        public ObservableCollection<string> ServiceTypes { get; }

        // ===== ВЫБРАННЫЕ ЭЛЕМЕНТЫ =====
        private Battery _selectedBattery;
        public Battery SelectedBattery
        {
            get => _selectedBattery;
            set => Set(ref _selectedBattery, value);
        }

        private Tire _selectedTire;
        public Tire SelectedTire
        {
            get => _selectedTire;
            set => Set(ref _selectedTire, value);
        }

        // ===== КОМАНДЫ =====
        public ICommand AddBatteryCommand { get; }
        public ICommand DeleteBatteryCommand { get; }
        public ICommand AddTireCommand { get; }
        public ICommand DeleteTireCommand { get; }
        public ICommand SaveCommand { get; }

        //===== КОНСТРУКТОР =====
        public AddCarViewModel(ICarService carService)
        {
            _carService = carService;

            Batteries = new ObservableCollection<Battery>();
            Tires = new ObservableCollection<Tire>();

            ServiceTypes = new ObservableCollection<string>();
            foreach (var service in System.Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            AddBatteryCommand = new RelayCommand(_ => AddBattery());
            DeleteBatteryCommand = new RelayCommand(_ => DeleteBattery(), _ => SelectedBattery != null);
            AddTireCommand = new RelayCommand(_ => AddTire());
            DeleteTireCommand = new RelayCommand(_ => DeleteTire(), _ => SelectedTire != null);
            SaveCommand = new RelayCommand(async _ => await SaveAsync());
        }

        private void AddBattery()
        {
            Batteries.Add(new Battery
            {
                Specifications = "",
                StartingCurrent = "",
                Polarity = "",
                BodyType = "",
                InstallationDate = DateTime.Now
            });
        }

        private void DeleteBattery()
        {
            if (SelectedBattery != null)
                Batteries.Remove(SelectedBattery);
        }

        private void AddTire()
        {
            Tires.Add(new Tire
            {
                Width = 0,
                Profile = null,
                Diameter = 0,
                Brand = "",
                Model = "",
                InstallationDate = DateTime.Now
            });
        }

        private void DeleteTire()
        {
            if (SelectedTire != null)
                Tires.Remove(SelectedTire);
        }

        private async Task SaveAsync()
        {
            if (Name == null || Model == null || Number == null || SelectedService == null)
            {
                MessageBox.Show("Поля: Наименование, Модель, Гос. номер и Служба должны быть заполнены",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var service = (ServiceType)System.Enum.Parse(typeof(ServiceType), SelectedService); //надо придумать значение по умолчанию

            var car = new Car
            {
                Name = Name,
                Model = Model,
                Number = Number,
                ClassName = ClassName ?? string.Empty,
                Service = service,
                Vin = Vin ?? string.Empty,
                Certificate = Certificate ?? string.Empty,
                EngineNumber = EngineNumber ?? string.Empty,
                Formular = Formular,
                FormularDate = FormularDate,
                Year = Year,
                InventoryNumber = InventoryNumber ?? string.Empty,
                Batteries = Batteries.ToList(),
                Tires = Tires.ToList()
            };

            await _carService.AddCarAsync(car);

            System.Windows.MessageBox.Show("Машина добавлена!", "Успех",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information);

            CloseAction?.Invoke();  // ← Закрыть окно
        }
    }
}