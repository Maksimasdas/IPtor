using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class AddPersonViewModel : ViewModelBase
    {
        private readonly IPersonService _personService;

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        private string _position;
        public string Position
        {
            get => _position;
            set => Set(ref _position, value);
        }

        private string _rank;
        public string Rank
        {
            get => _rank;
            set => Set(ref _rank, value);
        }

        private string _undPosition;
        public string UndPosition
        {
            get => _undPosition;
            set => Set(ref _undPosition, value);
        }

        private string _selectedService;
        public string SelectedService
        {
            get => _selectedService;
            set => Set(ref _selectedService, value);
        }

        public ObservableCollection<string> ServiceTypes { get; }
        public ICommand SaveCommand { get; }
        public Action CloseAction { get; set; }

        public AddPersonViewModel(IPersonService personService)
        {
            _personService = personService;

            ServiceTypes = new ObservableCollection<string>();
            foreach (var service in System.Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            SaveCommand = new RelayCommand(async _ => await SaveAsync(),
                _ => !string.IsNullOrWhiteSpace(Name));
        }

        private async Task SaveAsync()
        {
            var service = (ServiceType)System.Enum.Parse(typeof(ServiceType), SelectedService);

            var person = new Person
            {
                Name = Name,
                Position = Position ?? string.Empty,
                Rank = Rank ?? string.Empty,
                UndPosition = UndPosition ?? string.Empty,
                Service = service
            };

            await _personService.AddPersonAsync(person);

            MessageBox.Show("Сотрудник добавлен!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            CloseAction?.Invoke();
        }
    }
}
