using IPtor.Buisness.Interfaces;
using IPtor.Commands;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace IPtor.ViewModels
{
    public class EquipmentDetailViewModel : ViewModelBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly int _equipmentId;

        private Equipment _equipment;
        public Equipment Equipment
        {
            get => _equipment;
            set => Set(ref _equipment, value);
        }

        public ObservableCollection<string> ServiceTypes { get; }

        public ICommand SaveCommand { get; }
        public Action CloseAction { get; set; }

        public EquipmentDetailViewModel(IEquipmentService equipmentService, int equipmentId)
        {
            _equipmentService = equipmentService;
            _equipmentId = equipmentId;

            ServiceTypes = new ObservableCollection<string>();
            foreach (var service in Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>())
            {
                ServiceTypes.Add(service.ToString());
            }

            SaveCommand = new RelayCommand(async _ => await SaveAsync());

            _ = LoadEquipmentAsync();
        }

        private async Task LoadEquipmentAsync()
        {
            var equipment = await _equipmentService.GetEquipmentByIdAsync(_equipmentId);
            if (equipment != null)
                Equipment = equipment;
        }

        private async Task SaveAsync()
        {
            if (Equipment == null) return;

            await _equipmentService.UpdateEquipmentAsync(Equipment);

            MessageBox.Show("Изменения сохранены!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            CloseAction?.Invoke();
        }
    }
}

