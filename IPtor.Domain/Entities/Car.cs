using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using IPtor.Domain.Entities;

namespace IPtor.Domain.Entities
{
    public class Car
    {
        public int CarId { get; set; } //первичный ключ
        public string Name { get; set; } //наименование
        public string Model { get; set; } //модель
        public string Number { get; set; } //гос номер
        public string ClassName { get; set; } // наименование по штату (сокращенно)
        public ServiceType Service { get; set; } // служба
        public string Vin { get; set; } // вин номер
        public string Certificate { get; set; } // тех паспорт
        public string EngineNumber { get; set; } // номер двигателя
        public int Formular { get; set; } //номер формуляра
        public DateTime FormularDate { get; set; } //дата формуляра
        public int Year { get; set; } // год выпуска
        public string InventoryNumber { get; set; } // инвентарный номер
        public List<RepairRequest> RepairRequests { get; set; }  // List вместо ICollection

        // Навигационные свойства
        public ICollection<Battery> Batteries { get; set; }      // Коллекция АКБ
        public ICollection<Tire> Tires { get; set; } //коллекция шин
        //public ICollection<InspectionReport> InspectionReports { get; set; } //пока наверное не использовать
        public ICollection<Defects> Defects { get; set; } //дефекты при приемке на ремонт
        public ICollection<DefectsTO> DefectsTO { get; set; } //дефекты при составлении акта тех состояния

        public Car()
        {
            Batteries = new HashSet<Battery>();
            Tires = new HashSet<Tire>();
            Defects = new HashSet<Defects>();
            DefectsTO = new HashSet<DefectsTO>();
            RepairRequests = new List<RepairRequest>();

        }

        public override string ToString()
        {
            return $"{Name} - {Number}";
        }
    }
}
