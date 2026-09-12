using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class RepairsViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;
        private List<Car> _allRepairs;
        private ObservableCollection<Car> _repairs;
        public ObservableCollection<Car> Repairs
        {
            get => _repairs;
            set => Set(ref _repairs, value);
        }
        private Car _selectedRepair;
        public Car SelectedRepair
        {
            get => _selectedRepair;
            set => Set(ref _selectedRepair, value);
        }

        public ICommand LoadRepairsCommand { get; }
        public ICommand ReleaseFromRepairCommand { get; }
        public ICommand OpenRepairActCommand { get; }

        public RepairsViewModel(ICarService carService, IPersonService personService, IReportService reportService)
        {
            _carService = carService;
            _personService = personService;
            _reportService = reportService;

            LoadRepairsCommand = new RelayCommand(async _ => await LoadRepairsAsync());
            ReleaseFromRepairCommand = new RelayCommand(async _ => await ReleaseFromRepairAsync(),
                _ => SelectedRepair != null);
            OpenRepairActCommand = new RelayCommand(async _ => await OpenRepairActAsync(),
                _ => SelectedRepair != null);
        }

        private async Task OpenRepairActAsync()
        {
            if (SelectedRepair == null) return;

            var fullCar = await _carService.GetCarWithFullInfoAsync(SelectedRepair.CarId);

            // Ищем акт, созданный ПОСЛЕ отправки в ремонт
            var lastDefect = fullCar?.Defects?
                .Where(d => fullCar.RepairStartDate == null ||
                            d.ActDate >= fullCar.RepairStartDate.Value.AddSeconds(-5))
                .OrderByDescending(d => d.ActDate)
                .FirstOrDefault();

            if (lastDefect == null)
            {
                MessageBox.Show(
                    $"Автомобиль {SelectedRepair.Name} ({SelectedRepair.Number}) находится в ремонте без акта дефектовки.\n\n" +
                    "Создайте акт дефектовки для этого автомобиля.",
                    "Информация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            var actVM = new TransferActViewModel(_carService, _personService, _reportService, SelectedRepair);
            actVM.SetExistingDefect(lastDefect);
            await actVM.LoadPersonsAsync();
            actVM.ApplyExistingDefect();
            actVM.IsEditMode = true;

            var window = new TransferActView(actVM);
            window.ShowDialog();

            await LoadRepairsAsync();
        }

        private async Task ReleaseFromRepairAsync()
        {
            if (SelectedRepair == null) return;
            var result = MessageBox.Show(
                $"Выпустить автомобиль {SelectedRepair.Name} ({SelectedRepair.Number}) из ремонта?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                await _carService.SetRepairStatusAsync(SelectedRepair.CarId, false, null);
                await LoadRepairsAsync();
            }
        }

        public async Task LoadRepairsAsync()
        {
            var cars = await _carService.GetCarsWithDetailsAsync();
            _allRepairs = cars.Where(c => c.IsUnderRepair).ToList();
            Repairs = new ObservableCollection<Car>(_allRepairs);
        }
    }
}
