using IPtor.Commands;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // ===== ТЕКУЩАЯ АКТИВНАЯ ВКЛАДКА =====
        // Хранит ViewModel той вкладки, которая сейчас открыта
        // View будет привязан к этому свойству и показывать соответствующее содержимое
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => Set(ref _currentView, value);
        }


        // ===== КОМАНДЫ НАВИГАЦИИ =====
        // Каждая команда переключает CurrentView на нужную ViewModel
        public ICommand ShowCarsCommand { get; }
        public ICommand ShowPersonsCommand { get; }
        public ICommand ShowReportsCommand { get; }
        public ICommand ShowRepairRequestsCommand { get; }
        public ICommand ShowStatisticsCommand { get; }
        public ICommand ShowRepairsCommand { get; }

        // ===== КОНСТРУКТОР =====
        // Все дочерние ViewModel внедряются через DI
        public MainViewModel(CarsViewModel carsViewModel, PersonsViewModel personsViewModel, RepairRequestsViewModel repairRequestsViewModel, StatisticsViewModel statisticsViewModel, RepairsViewModel repairsViewModel)
        {
            // При старте показываем список машин
            CurrentView = carsViewModel;

            // Команда: переключиться на список машин
            ShowCarsCommand = new RelayCommand(_ => CurrentView = carsViewModel);
            ShowPersonsCommand = new RelayCommand(_ =>
            {
                CurrentView = personsViewModel;
                personsViewModel.LoadPersonsCommand.Execute(null);
            });

            // Команда: переключиться на заявки
            ShowRepairRequestsCommand = new RelayCommand(_ =>
            {
                CurrentView = repairRequestsViewModel;
                repairRequestsViewModel.LoadRequestsCommand.Execute(null);
            });

            //команда для статистики
            ShowStatisticsCommand = new RelayCommand(_ => CurrentView = statisticsViewModel);

            //добавлено после ПЗ
            ShowRepairsCommand = new RelayCommand(_ =>
            {
                CurrentView = repairsViewModel;
                _ = repairsViewModel.LoadRepairsAsync();
            });
        }
    }
}
