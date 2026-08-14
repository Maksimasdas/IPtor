using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class PersonDetailViewModel : ViewModelBase
    {
        private readonly IPersonService _personService;
        private readonly int _personId;

        private Person _person;
        public Person Person
        {
            get => _person;
            set => Set(ref _person, value);
        }

        public ObservableCollection<string> ServiceTypes { get; }
        public ICommand SaveCommand { get; }
        public Action CloseAction { get; set; }
        public PersonDetailViewModel(IPersonService personService, int personId)
        {
            _personService = personService;
            _personId = personId;

            ServiceTypes = new ObservableCollection<string>();
            foreach (var service in System.Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            SaveCommand = new RelayCommand(async _ => await SaveAsync());

            _ = LoadPersonAsync();
        }

        private async Task LoadPersonAsync()
        {
            var person = await _personService.GetPersonByIdAsync(_personId);
            if (person != null)
                Person = person;
        }

        private async Task SaveAsync()
        {
            if (Person == null) return;

            await _personService.UpdatePersonAsync(Person);

            System.Windows.MessageBox.Show("Изменения сохранены!", "Успех",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information);

            CloseAction?.Invoke();
        }
    }
}