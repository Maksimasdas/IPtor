using IPtor.Commands;
using IPtor.Helpers;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        // ===== ССЫЛКИ НА ДОЧЕРНИЕ VIEWMODEL =====
        // Нужны, чтобы Navigate() мог обращаться к ним из любого места
        private readonly CarsViewModel _carsViewModel;
        private readonly PersonsViewModel _personsViewModel;
        private readonly RepairRequestsViewModel _repairRequestsViewModel;
        private readonly StatisticsViewModel _statisticsViewModel;
        private readonly RepairsViewModel _repairsViewModel;
        private readonly EquipmentViewModel _equipmentViewModel;

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
        public ICommand ShowEquipmentCommand { get; }

        // ===== УНИВЕРСАЛЬНАЯ КОМАНДА (клавиатура) =====
        public ICommand NavigateCommand { get; }

        // ===== КОНСТРУКТОР =====
        // Все дочерние ViewModel внедряются через DI
        public MainViewModel(CarsViewModel carsViewModel, PersonsViewModel personsViewModel, RepairRequestsViewModel repairRequestsViewModel, 
            StatisticsViewModel statisticsViewModel, RepairsViewModel repairsViewModel, EquipmentViewModel equipmentViewModel)
        {
            _carsViewModel = carsViewModel;
            _personsViewModel = personsViewModel;
            _repairRequestsViewModel = repairRequestsViewModel;
            _statisticsViewModel = statisticsViewModel;
            _repairsViewModel = repairsViewModel;
            _equipmentViewModel = equipmentViewModel;
            // При старте показываем список машин
            CurrentView = carsViewModel;

            // Команда: переключиться на список машин (теперь делегируют в Navigate)
            ShowCarsCommand = new RelayCommand(_ => Navigate(NavigationTarget.Cars));
            ShowPersonsCommand = new RelayCommand(_ => Navigate(NavigationTarget.Persons));
            ShowRepairRequestsCommand = new RelayCommand(_ => Navigate(NavigationTarget.RepairRequests));
            ShowStatisticsCommand = new RelayCommand(_ => Navigate(NavigationTarget.Statistics));
            ShowRepairsCommand = new RelayCommand(_ => Navigate(NavigationTarget.Repairs));
            ShowEquipmentCommand = new RelayCommand(_ => Navigate(NavigationTarget.Equipment));

            // Универсальная команда для Ctrl+1…6
            NavigateCommand = new RelayCommand(param =>
            {
                if (param is NavigationTarget target)
                    Navigate(target);
            });
        }

        // ===== ЕДИНАЯ ТОЧКА ПЕРЕХОДА =====
        private void Navigate(NavigationTarget target)
        {
            switch (target)
            {
                case NavigationTarget.Cars:
                    CurrentView = _carsViewModel;
                    _carsViewModel.LoadCarsCommand.Execute(null);
                    break;

                case NavigationTarget.Persons:
                    CurrentView = _personsViewModel;
                    _personsViewModel.LoadPersonsCommand.Execute(null);
                    break;

                case NavigationTarget.RepairRequests:
                    CurrentView = _repairRequestsViewModel;
                    _repairRequestsViewModel.LoadRequestsCommand.Execute(null);
                    break;

                case NavigationTarget.Repairs:
                    CurrentView = _repairsViewModel;
                    _ = _repairsViewModel.LoadRepairsAsync();
                    break;

                case NavigationTarget.Equipment:
                    CurrentView = _equipmentViewModel;
                    _equipmentViewModel.LoadEquipmentCommand.Execute(null);
                    break;

                case NavigationTarget.Statistics:
                    CurrentView = _statisticsViewModel;
                    break;
            }
        }
    }
}
