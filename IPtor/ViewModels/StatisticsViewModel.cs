using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Enums;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
        public ICommand LoadDataCommand { get; }

        private ObservableCollection<ISeries> _serviceSeries;
        public ObservableCollection<ISeries> ServiceSeries
        {
            get => _serviceSeries;
            set => Set(ref _serviceSeries, value);
        }

        private ObservableCollection<ISeries> _yearSeries;
        public ObservableCollection<ISeries> YearSeries
        {
            get => _yearSeries;
            set => Set(ref _yearSeries, value);
        }

        private Axis[] _xAxes;
        public Axis[] XAxes
        {
            get => _xAxes;
            set => Set(ref _xAxes, value);
        }

        private Axis[] _yAxes;
        public Axis[] YAxes
        {
            get => _yAxes;
            set => Set(ref _yAxes, value);
        }

        public StatisticsViewModel(ICarService carService)
        {
            //System.Windows.MessageBox.Show("StatisticsViewModel создан");
            _carService = carService;
            //_ = LoadDataAsync();
            LoadDataCommand = new RelayCommand(async _ => await LoadDataAsync()); //справление бага не загрузки данныз для диагграмм
        }

        private async Task LoadDataAsync()
        {
            var cars = (await _carService.GetAllCarsAsync()).ToList();
            //System.Windows.MessageBox.Show($"Загружено машин: {cars.Count}"); // для проверки, потом удалю
            //исправление бага когда машины не подгружаются
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                // Pie
                var serviceGroups = cars
                    .GroupBy(c => c.Service)
                    .Select(g => new PieSeries<double>
                    {
                        Values = new double[] { g.Count() },
                        Name = g.Key.ToString()
                    })
                    .ToList();

                ServiceSeries = new ObservableCollection<ISeries>(serviceGroups);

                // Column
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

        }
    }   
}