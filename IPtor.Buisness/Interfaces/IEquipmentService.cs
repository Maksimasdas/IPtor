using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Interfaces
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetAllEquipmentAsync(); // получить все оборудование
        Task<Equipment> GetEquipmentByIdAsync(int id); // получить оборудование по ИД
        Task AddEquipmentAsync(Equipment equipment); // добавить оборудование
        Task UpdateEquipmentAsync(Equipment equipment); // обновить оборудование
        Task DeleteEquipmentAsync(int id); // удалить оборудование
    }
}
