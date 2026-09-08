using IPtor.Buisness.Interfaces;
using IPtor.Buisness.Models;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Helpers;
using IPtor.Views;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class TransferActViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;
        private readonly Car _car;

        //информация о машине
        public string CarInfo { get; }

        //список сотрудников
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set => Set(ref _persons, value);
        }

        //выбранные лица
        private Person _sheffPassService;
        public Person SheffPassService
        {
            get => _sheffPassService;
            set => Set(ref _sheffPassService, value);
        }

        private Person _responcePerson;
        public Person ResponcePerson
        {
            get => _responcePerson;
            set => Set(ref _responcePerson, value);
        }

        private Person _driver;
        public Person Driver
        {
            get => _driver;
            set => Set(ref _driver, value);
        }

        private Person _acceptUtoPerson;
        public Person AcceptUtoPerson
        {
            get => _acceptUtoPerson;
            set => Set(ref _acceptUtoPerson, value);
        }

        private Person _additionalPerson;
        public Person AdditionalPerson
        {
            get => _additionalPerson;
            set => Set(ref _additionalPerson, value);
        }

        //данные по эксплуатации
        private int _kmSpeedStart;
        public int KmSpeedStart
        {
            get => _kmSpeedStart;
            set => Set(ref _kmSpeedStart, value);
        }

        private string _mhSpeedStart; //изменил
        public string MhSpeedStart //изменил
        {
            get => _mhSpeedStart;
            set => Set(ref _mhSpeedStart, value);
        }

        private int _avarageRepair;
        public int AvarageRepair
        {
            get => _avarageRepair;
            set => Set(ref _avarageRepair, value);
        }

        private int _kapRepair;
        public int KapRepair
        {
            get => _kapRepair;
            set => Set(ref _kapRepair, value);
        }

        private int _kmSpeedKarRepair;
        public int KmSpeedKarRepair
        {
            get => _kmSpeedKarRepair;
            set => Set(ref _kmSpeedKarRepair, value);
        }

        private string _mhSpeedKapRepair; //изменил
        public string MhSpeedKapRepair //изменил
        {
            get => _mhSpeedKapRepair;
            set => Set(ref _mhSpeedKapRepair, value);
        }

        private int _kmSpeedTO;
        public int KmSpeedTO
        {
            get => _kmSpeedTO;
            set => Set(ref _kmSpeedTO, value);
        }

        private string _mhSpeedTO; //изменил
        public string MhSpeedTO //изменил
        {
            get => _mhSpeedTO;
            set => Set(ref _mhSpeedTO, value);
        }

        //дефекты
        private string _staff;
        public string Staff
        {
            get => _staff;
            set => Set(ref _staff, value);
        }

        private string _engineDefect;
        public string EngineDefect
        {
            get => _engineDefect;
            set => Set(ref _engineDefect, value);
        }

        private string _electro;
        public string Electro
        {
            get => _electro;
            set => Set(ref _electro, value);
        }

        private string _clutch;
        public string Clutch
        {
            get => _clutch;
            set => Set(ref _clutch, value);
        }

        private string _kpp;
        public string Kpp
        {
            get => _kpp;
            set => Set(ref _kpp, value);
        }

        private string _steering;
        public string Steering
        {
            get => _steering;
            set => Set(ref _steering, value);
        }

        private string _frontGear;
        public string FrontGear
        {
            get => _frontGear;
            set => Set(ref _frontGear, value);
        }

        private string _backGear;
        public string BackGear
        {
            get => _backGear;
            set => Set(ref _backGear, value);
        }

        private string _frame;
        public string Frame
        {
            get => _frame;
            set => Set(ref _frame, value);
        }

        private string _body;
        public string Body
        {
            get => _body;
            set => Set(ref _body, value);
        }

        private string _specEq;
        public string SpecEq
        {
            get => _specEq;
            set => Set(ref _specEq, value);
        }

        private string _externalCond;
        public string ExternalCond
        {
            get => _externalCond;
            set => Set(ref _externalCond, value);
        }

        private string _numberWheel;
        public string NumberWheel
        {
            get => _numberWheel;
            set => Set(ref _numberWheel, value);
        }

        private string _fuelAvail;
        public string FuelAvail
        {
            get => _fuelAvail;
            set => Set(ref _fuelAvail, value);
        }

        private string _conclusion;
        public string Conclusion
        {
            get => _conclusion;
            set => Set(ref _conclusion, value);
        }

        //команды
        public ICommand GenerateCommand { get; }
        public ICommand AddPersonCommand { get; }
        public Action CloseAction { get; set; }

        public TransferActViewModel(ICarService carService, IPersonService personService,
                                    IReportService reportService, Car car)
        {
            _carService = carService;
            _personService = personService;
            _reportService = reportService;
            _car = car;

            CarInfo = $"{car.Name} {car.Model} | Гос.номер: {car.Number} | VIN: {car.Vin}";

            GenerateCommand = new RelayCommand(_ => GenerateAct());
            AddPersonCommand = new RelayCommand(_ => OpenAddPersonWindow());

            _ = LoadPersonsAsync();
        }

        private async Task LoadPersonsAsync()
        {
            var persons = await _personService.GetAllPersonsAsync();
            Persons = new ObservableCollection<Person>(persons);
        }

        private void OpenAddPersonWindow()
        {
            var addPersonVM = new AddPersonViewModel(_personService);
            var window = new AddPersonView(addPersonVM);
            window.ShowDialog();

            _ = LoadPersonsAsync();
        }

        private async Task GenerateAct()
        {
            // Валидация
            if (SheffPassService == null || ResponcePerson == null || Driver == null ||
                AcceptUtoPerson == null || AdditionalPerson == null)
            {
                MessageBox.Show("Заполните все поля ответственных лиц", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Нормализация топлива — заменяем запятую на точку
            //float fuel = 0;
            //if (!string.IsNullOrWhiteSpace(FuelAvail))
            //{
            //    string normalized = FuelAvail.Replace(',', '.');
            //    float.TryParse(normalized, System.Globalization.NumberStyles.Any,
            //        System.Globalization.CultureInfo.InvariantCulture, out fuel);
            //}
            float fuel = NumberParser.ParseFloat(FuelAvail);
            //парсинг дробных чисел которые пришли как стринг
            float mhSpeedStart = NumberParser.ParseFloat(MhSpeedStart);
            float mhSpeedKapRepair = NumberParser.ParseFloat(MhSpeedKapRepair);
            float mhSpeedTO = NumberParser.ParseFloat(MhSpeedTO);
            // Собираем данные
            var data = new TransferActData
            {
                Car = _car,
                SheffPassService = SheffPassService,
                ResponcePerson = ResponcePerson,
                Driver = Driver,
                AcceptUtoPerson = AcceptUtoPerson,
                AdditionalPerson = AdditionalPerson,
                TransferDate = DateTime.Now,

                KmSpeedStart = KmSpeedStart,
                MhSpeedStart = mhSpeedStart,
                KmGeneral = KmSpeedStart + (int)(mhSpeedStart * 50),
                AvarageRepair = AvarageRepair,
                KapRepair = KapRepair,

                KmSpeedKarRepair = KmSpeedStart - KmSpeedKarRepair,
                MhSpeedKapRepair = mhSpeedStart - mhSpeedKapRepair,
                KmGeneralKapRepair = (KmSpeedStart - KmSpeedKarRepair) + (int)((mhSpeedStart - mhSpeedKapRepair) * 50),

                KmSpeedTO = KmSpeedStart - KmSpeedTO,
                MhSpeedTO = mhSpeedStart - mhSpeedTO,
                KmGeneralTO = (KmSpeedStart - KmSpeedTO) + (int)((mhSpeedStart - mhSpeedTO) * 50),

                Defects = new Defects
                {
                    Staff = Staff,
                    EngineDefect = EngineDefect,
                    Electro = Electro,
                    Clutch = Clutch,
                    Kpp = Kpp,
                    Steering = Steering,
                    FrontGear = FrontGear,
                    BackGear = BackGear,
                    Frame = Frame,
                    Body = Body,
                    SpecEq = SpecEq,
                    ExternalCond = ExternalCond,
                    NumberWheel = NumberWheel,
                    FuelAvail = fuel,
                    Conclusion = Conclusion
                }
            };

            // Диалог сохранения
            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                //FileName = $"Акт_передачи_{_car.Number}_{DateTime.Now:yyyyMMdd_HHmm}" 
                FileName = $"Акт_передачи_{_car.Number.Replace("/", "_")}_{DateTime.Now:yyyyMMdd_HHmm}"
            };

            if (saveDialog.ShowDialog() == true)
            {
                var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "Акт_передачи_шаблон.xlsx");

                _reportService.GenerateTransferAct(data, templatePath, saveDialog.FileName);

                MessageBox.Show("Акт успешно создан!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                try
                {
                    //System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    //{
                    //    FileName = saveDialog.FileName,
                    //    UseShellExecute = true
                    //});
                    FileHelper.OpenFile(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Файл сохранен, но не удалось открыть автоматически: {ex.Message}",
                        "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //сохранение истории акта
                var defects = new Defects
                {
                    CarId = _car.CarId,
                    ActDate = DateTime.Now,

                    //сотрудники
                    SheffPassServiceId = SheffPassService?.PersonId,
                    ResponcePersonId = ResponcePerson?.PersonId,
                    DriverId = Driver?.PersonId,
                    AcceptUtoPersonId = AcceptUtoPerson?.PersonId,
                    AdditionalPersonId = AdditionalPerson?.PersonId,

                    // Пробег и моточасы
                    KmSpeedStart = KmSpeedStart,
                    MhSpeedStart = mhSpeedStart,
                    
                    //дефекты
                    Staff = Staff,
                    EngineDefect = EngineDefect,
                    Electro = Electro,
                    Clutch = Clutch,
                    Kpp = Kpp,
                    Steering = Steering,
                    FrontGear = FrontGear,
                    BackGear = BackGear,
                    Frame = Frame,
                    Body = Body,
                    SpecEq = SpecEq,
                    ExternalCond = ExternalCond,
                    NumberWheel = NumberWheel,
                    FuelAvail = fuel,
                    Conclusion = Conclusion
                };
                await _carService.AddDefectsAsync(defects);

                CloseAction?.Invoke();
            }
        }
    }
}
