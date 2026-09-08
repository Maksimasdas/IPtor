using IPtor.Buisness.Interfaces;
using IPtor.Buisness.Models;
using IPtor.Buisness.Services;
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
    public class TOActViewModel : ViewModelBase
    {
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;
        private readonly ICarService _carService;
        private readonly Car _car;

        public string CarInfo { get; }
        public Action CloseAction { get; set; }

        // Сотрудники
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set => Set(ref _persons, value);
        }

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

        // Эксплуатация
        private string _kmSpeedometrStart; //изменил
        public string KmSpeedometrStart //изменил
        {
            get => _kmSpeedometrStart;
            set => Set(ref _kmSpeedometrStart, value);
        }

        private string _mhSpeedStart;//изменил
        public string MhSpeedStart//изменил
        {
            get => _mhSpeedStart;
            set => Set(ref _mhSpeedStart, value);
        }

        // Поля DefectsTO — тех. состояние
        private string _body;
        public string Body { get => _body; set => Set(ref _body, value); }

        private string _headBody;
        public string HeadBody { get => _headBody; set => Set(ref _headBody, value); }

        private string _engine;
        public string Engine { get => _engine; set => Set(ref _engine, value); }

        private string _kpp;
        public string KPP { get => _kpp; set => Set(ref _kpp, value); }

        private string _rul;
        public string Rul { get => _rul; set => Set(ref _rul, value); }

        private string _frontGear;
        public string FrontGear { get => _frontGear; set => Set(ref _frontGear, value); }

        private string _backGear;
        public string BackGear { get => _backGear; set => Set(ref _backGear, value); }

        private string _electro;
        public string Electro { get => _electro; set => Set(ref _electro, value); }

        private string _ptv;
        public string PTV { get => _ptv; set => Set(ref _ptv, value); }

        private string _water;
        public string Water { get => _water; set => Set(ref _water, value); }

        private string _generalView;
        public string GeneralView { get => _generalView; set => Set(ref _generalView, value); }

        private string _spec;
        public string Spec { get => _spec; set => Set(ref _spec, value); }

        private string _break;
        public string Break { get => _break; set => Set(ref _break, value); }

        private string _other;
        public string Other { get => _other; set => Set(ref _other, value); }

        // Укомплектованность
        private string _puskRuk;
        public string PuskRuk { get => _puskRuk; set => Set(ref _puskRuk, value); }

        private string _domkrat;
        public string Domkrat { get => _domkrat; set => Set(ref _domkrat, value); }

        private string _nasos;
        public string Nasos { get => _nasos; set => Set(ref _nasos, value); }

        private string _shpric;
        public string SHpric { get => _shpric; set => Set(ref _shpric, value); }

        private string _molotok;
        public string Molotok { get => _molotok; set => Set(ref _molotok, value); }

        private string _ploskogubci;
        public string Ploskogubci { get => _ploskogubci; set => Set(ref _ploskogubci, value); }

        private string _kluchi;
        public string Kluchi { get => _kluchi; set => Set(ref _kluchi, value); }

        private string _cluchi2;
        public string Cluchi2 { get => _cluchi2; set => Set(ref _cluchi2, value); }

        private string _cluchi3;
        public string Cluchi3 { get => _cluchi3; set => Set(ref _cluchi3, value); }

        private string _cluchi4;
        public string Cluchi4 { get => _cluchi4; set => Set(ref _cluchi4, value); }

        private string _chehli;
        public string Chehli { get => _chehli; set => Set(ref _chehli, value); }

        private string _aptechka;
        public string Aptechka { get => _aptechka; set => Set(ref _aptechka, value); }

        private string _znak;
        public string Znak { get => _znak; set => Set(ref _znak, value); }

        private string _disk;
        public string Disk { get => _disk; set => Set(ref _disk, value); }

        private string _ognetush;
        public string Ognetush { get => _ognetush; set => Set(ref _ognetush, value); }

        private string _radio;
        public string Radio { get => _radio; set => Set(ref _radio, value); }

        private string _protivougon;
        public string Protivougon { get => _protivougon; set => Set(ref _protivougon, value); }

        private string _gromkogov;
        public string Gromkogov { get => _gromkogov; set => Set(ref _gromkogov, value); }

        private string _protivotuman;
        public string Protivotuman { get => _protivotuman; set => Set(ref _protivotuman, value); }

        private string _mayak;
        public string Mayak { get => _mayak; set => Set(ref _mayak, value); }

        private string _fara;
        public string Fara { get => _fara; set => Set(ref _fara, value); }

        private string _prochee;
        public string Prochee { get => _prochee; set => Set(ref _prochee, value); }

        private string _fuel;
        public string Fuel { get => _fuel; set => Set(ref _fuel, value); }

        private string _zakl;
        public string Zakl { get => _zakl; set => Set(ref _zakl, value); }

        private string _pred;
        public string Pred { get => _pred; set => Set(ref _pred, value); }

        public ICommand GenerateCommand { get; }
        public ICommand AddPersonCommand { get; }

        public TOActViewModel(IPersonService personService, IReportService reportService, ICarService carService, Car car)
        {
            _personService = personService;
            _reportService = reportService;
            _carService = carService;
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
            if (SheffPassService == null || ResponcePerson == null || Driver == null)
            {
                MessageBox.Show("Заполните все поля ответственных лиц", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //float fuel = 0;
            //if (!string.IsNullOrWhiteSpace(Fuel))
            //{
            //    string normalized = Fuel.Replace(',', '.');
            //    float.TryParse(normalized, System.Globalization.NumberStyles.Any,
            //        System.Globalization.CultureInfo.InvariantCulture, out fuel);
            //}
            float fuel = NumberParser.ParseFloat(Fuel);
            int kmSpeedStart = NumberParser.ParseInt(KmSpeedometrStart);
            float mhSpeedStart = NumberParser.ParseFloat(MhSpeedStart);

            var data = new TOAktData
            {
                Car = _car,
                SheffPassService = SheffPassService,
                ResponcePerson = ResponcePerson,
                Driver = Driver,
                TransferDate = DateTime.Now,
                KmSpeedometrStart = kmSpeedStart,
                MhSpeedStart = mhSpeedStart,
                DefectsTO = new DefectsTO
                {
                    Body = Body,
                    HeadBody = HeadBody,
                    Engine = Engine,
                    KPP = KPP,
                    Rul = Rul,
                    FrontGear = FrontGear,
                    BackGear = BackGear,
                    Electro = Electro,
                    PTV = PTV,
                    Water = Water,
                    GeneralView = GeneralView,
                    Spec = Spec,
                    Break = Break,
                    Other = Other,
                    PuskRuk = PuskRuk,
                    Domkrat = Domkrat,
                    Nasos = Nasos,
                    SHpric = SHpric,
                    Molotok = Molotok,
                    Ploskogubci = Ploskogubci,
                    Kluchi = Kluchi,
                    Cluchi2 = Cluchi2,
                    Cluchi3 = Cluchi3,
                    Cluchi4 = Cluchi4,
                    Chehli = Chehli,
                    Aptechka = Aptechka,
                    Znak = Znak,
                    Disk = Disk,
                    Ognetush = Ognetush,
                    Radio = Radio,
                    Protivougon = Protivougon,
                    Gromkogov = Gromkogov,
                    Protivotuman = Protivotuman,
                    Mayak = Mayak,
                    Fara = Fara,
                    Prochee = Prochee,
                    Fuel = fuel,
                    Zakl = Zakl,
                    Pred = Pred
                }
            };

            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                //FileName = $"Акт_Тех_Состояния_{_car.Number}_{DateTime.Now:yyyyMMdd_HHmm}"
                FileName = $"Акт_Тех_Состояния_{_car.Number.Replace("/", "_")}_{DateTime.Now:yyyyMMdd_HHmm}"
            };

            if (saveDialog.ShowDialog() == true)
            {
                var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "Акт_передачи_шаблон2.xlsx");

                _reportService.GenerateTOAct(data, templatePath, saveDialog.FileName);

                MessageBox.Show("Акт успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

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
                //для сохранения истории акта

                var defectsTO = new DefectsTO
                {
                    CarId = _car.CarId,
                    ActDate = DateTime.Now,

                    // Сотрудники
                    SheffPassServiceId = SheffPassService?.PersonId,
                    ResponcePersonId = ResponcePerson?.PersonId,
                    DriverId = Driver?.PersonId,

                    // Пробег и моточасы
                    KmSpeedometrStart = kmSpeedStart,
                    MhSpeedStart = mhSpeedStart,

                    //остальное
                    Body = Body,
                    HeadBody = HeadBody,
                    Engine = Engine,
                    KPP = KPP,
                    Rul = Rul,
                    FrontGear = FrontGear,
                    BackGear = BackGear,
                    Electro = Electro,
                    PTV = PTV,
                    Water = Water,
                    GeneralView = GeneralView,
                    Spec = Spec,
                    Break = Break,
                    Other = Other,
                    PuskRuk = PuskRuk,
                    Domkrat = Domkrat,
                    Nasos = Nasos,
                    SHpric = SHpric,
                    Molotok = Molotok,
                    Ploskogubci = Ploskogubci,
                    Kluchi = Kluchi,
                    Cluchi2 = Cluchi2,
                    Cluchi3 = Cluchi3,
                    Cluchi4 = Cluchi4,
                    Chehli = Chehli,
                    Aptechka = Aptechka,
                    Znak = Znak,
                    Disk = Disk,
                    Ognetush = Ognetush,
                    Radio = Radio,
                    Protivougon = Protivougon,
                    Gromkogov = Gromkogov,
                    Protivotuman = Protivotuman,
                    Mayak = Mayak,
                    Fara = Fara,
                    Prochee = Prochee,
                    Fuel = fuel,
                    Zakl = Zakl,
                    Pred = Pred
                };

                await _carService.AddDefectsTOAsync(defectsTO);


                CloseAction?.Invoke();
            }
        }
    }
}
