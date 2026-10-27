using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Enums;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly ICarService _carService;
        public ICommand LoadDataCommand { get; } //для исправления глюка, теперь загрузка данныхх для моделей идут через команду привязанную к событию onloaded

        private ObservableCollection<ISeries> _serviceSeries;
        public ObservableCollection<ISeries> ServiceSeries //график машин по службам
        {
            get => _serviceSeries;
            set => Set(ref _serviceSeries, value);
        }

        private ObservableCollection<ISeries> _yearSeries;
        public ObservableCollection<ISeries> YearSeries //графиик машин по годам
        {
            get => _yearSeries;
            set => Set(ref _yearSeries, value);
        }

        //добавлено после ПЗ
        private ObservableCollection<ISeries> _repairSeries;
        public ObservableCollection<ISeries> RepairSeries
        {
            get => _repairSeries;
            set => Set(ref _repairSeries, value);
        }

        private Axis[] _xAxes;
        public Axis[] XAxes //ось X
        {
            get => _xAxes;
            set => Set(ref _xAxes, value);
        }

        private Axis[] _yAxes;
        public Axis[] YAxes //ось Y
        {
            get => _yAxes;
            set => Set(ref _yAxes, value);
        }

        //добавлено после ПЗ
        private Axis[] _repairXAxes;
        public Axis[] RepairXAxes
        {
            get => _repairXAxes;
            set => Set(ref _repairXAxes, value);
        }
        private Axis[] _repairYAxes;
        public Axis[] RepairYAxes
        {
            get => _repairYAxes;
            set => Set(ref _repairYAxes, value);
        }
        public StatisticsViewModel(ICarService carService)
        {
            _carService = carService;
            //_ = LoadDataAsync();
            LoadDataCommand = new RelayCommand(async _ => await LoadDataAsync()); //исправление бага не загрузки данныз для диагграмм
        }

        private async Task LoadDataAsync() //загрузка данных и формирование диаграмм
        {
            var cars = (await _carService.GetAllCarsAsync()).ToList();
            //System.Windows.MessageBox.Show($"Загружено машин: {cars.Count}"); // для проверки, потом удалю
            //исправление бага когда машины не подгружаются
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                // Pie график машин по службам
                var serviceGroups = cars
                    .GroupBy(c => c.Service)
                    .Select(g => new PieSeries<double>
                    {
                        Values = new double[] { g.Count() },
                        Name = g.Key.ToString(),
                        DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle, //для того что бы показывались количество машин в графике
                        DataLabelsSize = 12, //размер цифр
                        DataLabelsPaint = new SolidColorPaint(SKColors.White)
                    })
                    .ToList();

                ServiceSeries = new ObservableCollection<ISeries>(serviceGroups);

                // Column график машин по годам
                var yearGroups = cars
                    .GroupBy(c => c.Year)
                    .OrderBy(g => g.Key)
                    .ToList();

                var yearValues = new ColumnSeries<int>
                {
                    Values = yearGroups.Select(g => g.Count()).ToArray(),
                    Name = "Количество машин"
                };

                YearSeries = new ObservableCollection<ISeries> { yearValues };

                XAxes = new Axis[]
                {
                    new Axis
                    {
                        Labels = yearGroups.Select(g => g.Key.ToString()).ToArray()
                    }
                };

                YAxes = new Axis[]
                {
                    new Axis
                    {
                        MinLimit = 0,
                        MinStep = 1,
                        Labeler = value => value.ToString("0")
                    }
                };
            });

            // График машин в ремонте (по дням)
            var carsInRepair = cars
                .Where(c => c.IsUnderRepair && c.RepairStartDate.HasValue)
                .Select(c => new
                {
                    Name = $"{c.Name} {c.Number}",
                    Days = (DateTime.Now - c.RepairStartDate.Value).TotalDays
                })
                .OrderByDescending(c => c.Days)
                .ToList();

            var repairValues = new RowSeries<double>
            {
                Values = carsInRepair.Select(c => Math.Round(c.Days, 1)).ToArray(),
                Name = "Дней в ремонте",
                DataLabelsSize = 12,
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.End,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),

                // Кастомный tooltip
                //XToolTipLabelFormatter = point =>
                //{
                //    int index = (int)point.Coordinate.SecondaryValue;
                //    var carName = carsInRepair.ElementAtOrDefault(index)?.Name ?? "—";
                //    return $"{carName}\n{"".PadLeft(carName.Length / 2)}{point.Coordinate.PrimaryValue:F1} дн.";
                //}
            };

            RepairSeries = new ObservableCollection<ISeries> { repairValues };

            RepairYAxes = new Axis[]
            {
                new Axis
                {
                    Labels = carsInRepair.Select(c => c.Name).ToArray(),
                    TextSize = 11
                }
                        };

                        RepairXAxes = new Axis[]
                        {
                new Axis
                {
                    MinLimit = 0,
                    Labeler = value => value.ToString("0.#") + " дн."
                }
                        };

        }
    }   
}