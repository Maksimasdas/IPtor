using IPtor.Domain.Enums;
using System;

namespace IPtor.Domain.Entities
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; } // Stihl
        public string Model { get; set; } // FS-500
        public string? SerialNumber { get; set; } //серийный номер
        public string ClassName { get; set; } // бензорез, бензопила
        public ServiceType Service { get; set; } //служба
        public int Formular { get; set; } //номер формуляра
        public DateTime FormularDate { get; set; } //дата формуляра
        public int? Year { get; set; } // год выпуска
        public string? InventoryNumber { get; set; } // инвентарник
        public int? BoxNumber { get; set; } // номер ящика для инсарага
        public decimal? Amount { get; set; } // стоимость
        public string? CountryOfOrigin { get; set; } // страна происхождения
        public float? Weight { get; set; } // вес брутто
        public string? TNVEDCode { get; set; } //код ТЭНВЭД
    }
}