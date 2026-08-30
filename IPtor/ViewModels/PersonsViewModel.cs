using DocumentFormat.OpenXml.Vml.Office;
using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using IPtor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class PersonsViewModel : ViewModelBase
    {
        private readonly IPersonService _personService;
        private List<Person> _allPersons;

        //===коллекции===
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set => Set(ref _persons, value);
        }

        //===выбранный=== 
        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => Set(ref _selectedPerson, value);
        }

        //=== поиск ===
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        //=== поля для нового сотрудника
        private string _newName;
        public string NewName
        {
            get => _newName;
            set => Set(ref _newName, value);
        }

        private string _newPosition;
        public string NewPosition
        {
            get => _newPosition;
            set => Set(ref _newPosition, value);
        }

        private string _newRank;
        public string NewRank
        {
            get => _newRank;
            set => Set(ref _newRank, value);
        }

        private string _newUndPosition;
        public string NewUndPosition
        {
            get => _newUndPosition;
            set => Set(ref _newUndPosition, value);
        }

        private string _newService;
        public string NewService
        {
            get => _newService;
            set => Set(ref _newService, value); //проверку на нулл сделать
        }

        // === список служб ===
        public ObservableCollection<string> ServiceTypes { get; }

        //=== команды ===
        public ICommand LoadPersonsCommand { get; }
        public ICommand AddPersonCommand { get; }
        public ICommand DeletePersonCommand { get; }
        public ICommand OpenPersonDetailCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public PersonsViewModel(IPersonService personService)
        {
            _personService = personService;

            ServiceTypes = new ObservableCollection<string>();
            foreach(var service in System.Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            LoadPersonsCommand = new RelayCommand(async _ => await LoadPersonsAsync());
            AddPersonCommand = new RelayCommand(async _ => await AddPersonAsync(), __ => !string.IsNullOrWhiteSpace(NewName));
            DeletePersonCommand = new RelayCommand(async _ => await DeletePersonAsync(), _ => SelectedPerson != null);
            OpenPersonDetailCommand = new RelayCommand(_ => OpenPersonDetail(), _ => SelectedPerson != null);
            SearchCommand = new RelayCommand(_ => SearchPersons());
            ClearSearchCommand = new RelayCommand(_ => ClearSearch());

            _ = LoadPersonsAsync();
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            if (_allPersons != null)
                SearchPersons();
        }

        private async Task LoadPersonsAsync()
        {
            var persons = await _personService.GetAllPersonsAsync();
            _allPersons = persons.ToList();
            Persons = new ObservableCollection<Person>(persons);
        }

        private void SearchPersons()
        {
            if (_allPersons == null) return; //защита на всякий случай

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Persons = new ObservableCollection<Person>(_allPersons);
                return;
            }

            var filtered = _allPersons
                .Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Persons = new ObservableCollection<Person>(filtered);
        }
        private async Task AddPersonAsync()
        {
            ServiceType service;
            if (string.IsNullOrEmpty(NewService))
            {
                service = ServiceType.ОМТОиС;
            }
            else
            {
                service = (ServiceType)System.Enum.Parse(typeof(ServiceType), NewService);
            }

            var person = new Person
            {
                Name = NewName,
                Position = NewPosition ?? string.Empty,
                Rank = NewRank ?? string.Empty,
                UndPosition = NewUndPosition ?? string.Empty,
                Service = service
            };

            await _personService.AddPersonAsync(person);

            NewName = string.Empty;
            NewPosition = string.Empty;
            NewRank = string.Empty;
            NewUndPosition = string.Empty;

            await LoadPersonsAsync();
        }

        private async Task DeletePersonAsync()
        {
            if (SelectedPerson == null) return;

            MessageBoxResult result = MessageBox.Show(
                    $"Вы действительно хотите удалить сотрудника {_selectedPerson.Name}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                await _personService.DeletePersonAsync(SelectedPerson.PersonId);
                Persons.Remove(SelectedPerson);
            }

            //await _personService.DeletePersonAsync(SelectedPerson.PersonId);
            //Persons.Remove(SelectedPerson);
        }

        private void OpenPersonDetail()
        {
            if (SelectedPerson == null) return;

            var detailVM = new PersonDetailViewModel(_personService, SelectedPerson.PersonId);
            var window = new PersonDetailView(detailVM);
            window.ShowDialog();

            _ = LoadPersonsAsync();
        }
    }
}
