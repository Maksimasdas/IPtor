using IPtor.Buisness.Interfaces;
using IPtor.Buisness.Models;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using IPtor.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace IPtor.ViewModels
{
    public class CarDetailViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        private readonly IReportService _reportService;
        private readonly int _carId;

        public Action CloseAction { get; set; } //для закрытия окна после добавлнеия машины

        private Car _car;
        public Car Car
        {
            get => _car;
            set => Set(ref _car, value);
        }

        public ObservableCollection<Battery> Batteries { get; set; }
        public ObservableCollection<Tire> Tires { get; set; }
        public ObservableCollection<Defects> Defects { get; set; }
        public ObservableCollection<DefectsTO> DefectsTO { get; set; }
        public ObservableCollection<string> ServiceTypes { get; } //добавлено потому что было сразу пропущено

        //команда сохранения изменений
        public ICommand SaveCommand { get; }

        // Команды для батарей
        public ICommand AddBatteryCommand { get; }
        public ICommand DeleteBatteryCommand { get; }

        // Команды для шин
        public ICommand AddTireCommand { get; }
        public ICommand DeleteTireCommand { get; }

        //кодманда для обновления списка истории актов
        public ICommand RefreshCommand { get; }

        //команды для актов
        public ICommand OpenDefectActCommand { get; }
        public ICommand OpenTOActCommand { get; }

        // Выбранные элементы
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

        //выбранные акты
        private Defects _selectedDefect;
        public Defects SelectedDefect
        {
            get => _selectedDefect;
            set => Set(ref _selectedDefect, value);
        }
        private DefectsTO _selectedDefectTO;
        public DefectsTO SelectedDefectTO
        {
            get => _selectedDefectTO;
            set => Set(ref _selectedDefectTO, value);
        }

        public CarDetailViewModel(ICarService carService, IReportService reportService, int carId)
        {
            _carService = carService;
            _reportService = reportService;
            _carId = carId;

            //помоему не хватает заполнения комбобокса служб
            ServiceTypes = new ObservableCollection<string>();
            foreach (var service in System.Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            Batteries = new ObservableCollection<Battery>();
            Tires = new ObservableCollection<Tire>();
            Defects = new ObservableCollection<Defects>();
            DefectsTO = new ObservableCollection<DefectsTO>();

            SaveCommand = new RelayCommand(async _ => await SaveAsync());

            AddBatteryCommand = new RelayCommand(_ => AddBattery());
            DeleteBatteryCommand = new RelayCommand(_ => DeleteBattery(), _ => SelectedBattery != null);

            AddTireCommand = new RelayCommand(_ => AddTire());
            DeleteTireCommand = new RelayCommand(_ => DeleteTire(), _ => SelectedTire != null);
            RefreshCommand = new RelayCommand(async _ => await LoadCarAsync());

            OpenDefectActCommand = new RelayCommand(_ => OpenDefectAct(), _ => SelectedDefect != null);
            OpenTOActCommand = new RelayCommand(_ => OpenTOAct(), _ => SelectedDefectTO != null);

            _ = LoadCarAsync();
        }

        private async Task LoadCarAsync()
        {
            var car = await _carService.GetCarWithFullInfoAsync(_carId);
            if (car == null) return;

            Car = car;

            Batteries.Clear();
            Tires.Clear();
            Defects.Clear();
            DefectsTO.Clear();

            foreach (var b in car.Batteries) Batteries.Add(b);
            foreach (var t in car.Tires) Tires.Add(t);
            foreach (var d in car.Defects) Defects.Add(d);
            foreach (var dt in car.DefectsTO) DefectsTO.Add(dt);
        }

        private async Task SaveAsync()
        {
            if (Car == null) return; 

            // Обновляем машину
            await _carService.UpdateCarAsync(Car);

            // Добавляем новые батареи и шины
            foreach (var battery in Batteries)
            {
                if (battery.BatteryId == 0)  // Новая запись
                    await _carService.AddBatteryAsync(battery);
                else
                    await _carService.UpdateBatteryAsync(battery); //если не новая, то обновляем
            }

            foreach (var tire in Tires)
            {
                if (tire.TireId == 0)  // Новая запись
                    await _carService.AddTireAsync(tire);
                else
                    await _carService.UpdateTireAsync(tire); //если не новая, то обновляем
            }

            System.Windows.MessageBox.Show("Изменения сохранены!", "Успех",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information);



            CloseAction?.Invoke();  // ← Закрыть окно
        }
        private void AddBattery()
        {
            var battery = new Battery
            {
                CarId = _carId,
                Specifications = "Новая",
                StartingCurrent = "",
                Polarity = "",
                BodyType = "",
                InstallationDate = System.DateTime.Now
            };
            Batteries.Add(battery);
        }

        private async void DeleteBattery()
        {
            if (SelectedBattery == null) return;

            if (SelectedBattery.BatteryId != 0)
                await _carService.DeleteBatteryAsync(SelectedBattery.BatteryId);

            Batteries.Remove(SelectedBattery);
        }

        private void AddTire()
        {
            var tire = new Tire
            {
                CarId = _carId,
                Width = 0,
                Profile = null,
                Diameter = 0,
                Brand = "Новая",
                Model = "",
                InstallationDate = System.DateTime.Now
            };
            Tires.Add(tire);
        }

        private async void DeleteTire()
        {
            if (SelectedTire == null) return;

            if (SelectedTire.TireId != 0)
                await _carService.DeleteTireAsync(SelectedTire.TireId);

            Tires.Remove(SelectedTire);
        }

        //методы для восстановления акта из истории
        private void OpenDefectAct()
        {
            if (SelectedDefect == null || Car == null) return;

            var data = new TransferActData
            {
                Car = Car,
                TransferDate = SelectedDefect.ActDate,
                SheffPassService = SelectedDefect.SheffPassService,
                ResponcePerson = SelectedDefect.ResponcePerson,
                Driver = SelectedDefect.Driver,
                AcceptUtoPerson = SelectedDefect.AcceptUtoPerson,
                AdditionalPerson = SelectedDefect.AdditionalPerson,
                KmSpeedStart = SelectedDefect.KmSpeedStart,
                MhSpeedStart = SelectedDefect.MhSpeedStart,
                KmGeneral = SelectedDefect.KmSpeedStart + (int)(SelectedDefect.MhSpeedStart * 50),

                // Вычисляемые значения (разница)
                KmSpeedKarRepair = SelectedDefect.KmSpeedStart - SelectedDefect.KmSpeedKarRepair,
                MhSpeedKapRepair = SelectedDefect.MhSpeedStart - SelectedDefect.MhSpeedKapRepair,
                KmGeneralKapRepair = (SelectedDefect.KmSpeedStart - SelectedDefect.KmSpeedKarRepair)
                       + (int)((SelectedDefect.MhSpeedStart - SelectedDefect.MhSpeedKapRepair) * 50),

                KmSpeedTO = SelectedDefect.KmSpeedStart - SelectedDefect.KmSpeedTO,
                MhSpeedTO = SelectedDefect.MhSpeedStart - SelectedDefect.MhSpeedTO,
                KmGeneralTO = (SelectedDefect.KmSpeedStart - SelectedDefect.KmSpeedTO)
                + (int)((SelectedDefect.MhSpeedStart - SelectedDefect.MhSpeedTO) * 50),

                AvarageRepair = SelectedDefect.AvarageRepair,
                KapRepair = SelectedDefect.KapRepair,

                Defects = SelectedDefect

            };
            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                FileName = $"Акт_передачи_{Car.Number.Replace("/", "_")}_{DateTime.Now:yyyyMMdd_HHmm}"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "Акт_передачи_шаблон.xlsx");
                _reportService.GenerateTransferAct(data, templatePath, saveDialog.FileName);
                FileHelper.OpenFile(saveDialog.FileName);
            }
        }

        private void OpenTOAct()
        {
            if (SelectedDefect == null || Car == null) return;

            var data = new TOAktData
            {
                Car = Car,
                TransferDate = SelectedDefectTO.ActDate,
                SheffPassService = SelectedDefectTO.SheffPassService,
                ResponcePerson = SelectedDefectTO.ResponcePerson,
                Driver = SelectedDefectTO.Driver,
                KmSpeedometrStart = SelectedDefectTO.KmSpeedometrStart,
                MhSpeedStart = SelectedDefectTO.MhSpeedStart,
                DefectsTO = SelectedDefectTO
            };
            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                FileName = $"Акт_ТО_{Car.Number.Replace("/", "_")}_{DateTime.Now:yyyyMMdd_HHmm}"
            };

            if (saveDialog.ShowDialog() == true)
            {
                var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "Акт_передачи_шаблон2.xlsx");
                _reportService.GenerateTOAct(data, templatePath, saveDialog.FileName);
                FileHelper.OpenFile(saveDialog.FileName);
            }
        }
    }
}
