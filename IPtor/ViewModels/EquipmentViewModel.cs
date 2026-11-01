using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using IPtor.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IPtor.Helpers;

namespace IPtor.ViewModels
{
    public class EquipmentViewModel : ViewModelBase
    {
        private readonly IEquipmentService _equipmentService;
        private List<Equipment> _allEquipment;
        private ObservableCollection<Equipment> _equipment;
        public ObservableCollection<Equipment> Equipment
        {
            get => _equipment;
            set => Set(ref _equipment, value);
        }
        private Equipment _selectedEquipment;
        public Equipment SelectedEquipment
        {
            get => _selectedEquipment;
            set => Set(ref _selectedEquipment, value);
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
                    ApplyFilters();
                }
            }
        }

        // ===== ПОИСК =====
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        private string _newName;
        public string NewName
        {
            get => _newName;
            set => Set(ref _newName, value);
        }
        private string _newModel;
        public string NewModel
        {
            get => _newModel;
            set => Set(ref _newModel, value);
        }
        private string _newClassName;
        public string NewClassName
        {
            get => _newClassName;
            set => Set(ref _newClassName, value);
        }
        private string _newSerialNumber;
        public string NewSerialNumber
        {
            get => _newSerialNumber;
            set => Set(ref _newSerialNumber, value);
        }
        private string _newService;
        public string NewService
        {
            get => _newService;
            set => Set(ref _newService, value);
        }
        private int _newFormular;
        public int NewFormular
        {
            get => _newFormular;
            set => Set(ref _newFormular, value);
        }
        private int _newYear;
        public int NewYear
        {
            get => _newYear;
            set => Set(ref _newYear, value);
        }
        private string _newInventoryNumber;
        public string NewInventoryNumber
        {
            get => _newInventoryNumber;
            set => Set(ref _newInventoryNumber, value);
        }
        private int _newBoxNumber;
        public int NewBoxNumber
        {
            get => _newBoxNumber;
            set => Set(ref _newBoxNumber, value);
        }

        public ObservableCollection<string> ServiceTypes { get; }

        //КОМАНДЫ
        public ICommand LoadEquipmentCommand { get; }
        public ICommand AddEquipmentCommand { get; }
        public ICommand DeleteEquipmentCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand OpenEquipmentDetailCommand { get; }


        //КОНСТРУКТОР
        public EquipmentViewModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            //ServiceTypes = new ObservableCollection<string> { "Все службы" };
            //foreach (var service in Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            //{
            //    ServiceTypes.Add(service.ToString());
            //}

            ServiceTypes = new ObservableCollection<string>(EnumHelper.GetServiceTypesWithAll());

            LoadEquipmentCommand = new RelayCommand(async _ => await LoadEquipmentAsync());
            AddEquipmentCommand = new RelayCommand(async _ => await AddEquipmentAsync(),
                _ => !string.IsNullOrWhiteSpace(NewName) && !string.IsNullOrWhiteSpace(NewModel));
            DeleteEquipmentCommand = new RelayCommand(async _ => await DeleteEquipmentAsync(),
                _ => SelectedEquipment != null);
            SearchCommand = new RelayCommand(_ => ApplyFilters());
            ClearSearchCommand = new RelayCommand(_ => ClearSearch());
            OpenEquipmentDetailCommand = new RelayCommand(_ => OpenEquipmentDetail(),
                _ => SelectedEquipment != null);

            _ = LoadEquipmentAsync();
        }

        //методы
        private async Task LoadEquipmentAsync()
        {
            var list = await _equipmentService.GetAllEquipmentAsync();
            _allEquipment = list.ToList();
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (_allEquipment == null) return;

            IEnumerable<Equipment> filtered = _allEquipment;

            // Фильтр по службе
            if (!string.IsNullOrEmpty(SelectedServiceFilter) && SelectedServiceFilter != "Все службы")
            {
                if (Enum.TryParse<ServiceType>(SelectedServiceFilter, out var service))
                {
                    filtered = filtered.Where(e => e.Service == service);
                }
            }
            // Поиск по тексту
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(e =>
                    (e.Name != null && e.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (e.Model != null && e.Model.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (e.SerialNumber != null && e.SerialNumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (e.ClassName != null && e.ClassName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (e.InventoryNumber != null && e.InventoryNumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }

            Equipment = new ObservableCollection<Equipment>(filtered);
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            SelectedServiceFilter = "Все службы";
            _ = LoadEquipmentAsync();
        }

        private async Task AddEquipmentAsync()
        {
            if (string.IsNullOrWhiteSpace(NewService))
            {
                MessageBox.Show("Необходимо выбрать службу", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //var service = (ServiceType)Enum.Parse(typeof(ServiceType), NewService);
            if (!Enum.TryParse<ServiceType>(NewService, out var service))
            {
                MessageBox.Show("Некорректная служба", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var equipment = new Equipment
            {
                Name = NewName,
                Model = NewModel,
                ClassName = NewClassName ?? string.Empty,
                SerialNumber = NewSerialNumber,
                Service = service,
                Formular = NewFormular,
                FormularDate = DateTime.Now,
                Year = NewYear > 0 ? NewYear : null,
                InventoryNumber = NewInventoryNumber,
                BoxNumber = NewBoxNumber > 0 ? NewBoxNumber : null
            };

            await _equipmentService.AddEquipmentAsync(equipment);

            // Очистка полей
            NewName = string.Empty;
            NewModel = string.Empty;
            NewClassName = string.Empty;
            NewSerialNumber = string.Empty;
            NewInventoryNumber = string.Empty;
            NewFormular = 0;
            NewYear = 0;
            NewBoxNumber = 0;

            await LoadEquipmentAsync();
        }



        private async Task DeleteEquipmentAsync()
        {
            if (SelectedEquipment == null) return;

            var result = MessageBox.Show(
                $"Удалить оборудование {SelectedEquipment.Name} {SelectedEquipment.Model}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await _equipmentService.DeleteEquipmentAsync(SelectedEquipment.EquipmentId);
                await LoadEquipmentAsync();
            }
        }

        private void OpenEquipmentDetail()
        {
            if (SelectedEquipment == null) return;

            var detailVM = new EquipmentDetailViewModel(_equipmentService, SelectedEquipment.EquipmentId);
            var window = new EquipmentDetailView(detailVM);
            window.ShowDialog();

            _ = LoadEquipmentAsync();
        }

    }
}
