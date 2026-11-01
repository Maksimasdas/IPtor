using DocumentFormat.OpenXml.VariantTypes;
using IPtor.Buisness.Interfaces;
using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _uow;
        public EquipmentService(IUnitOfWork uow)
        {
            _uow = uow; //остановился тут, исправил косяк надо дальше
        }

        public async Task AddEquipmentAsync(Equipment equipment)
        {
            await _uow.Equipment.AddAsync(equipment);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteEquipmentAsync(int id)
        {
            var equipment = await _uow.Equipment.GetByIdAsync(id);
            if (equipment == null) return;

            //проверки всякие пока тоже не нужны, просто удаляем

            _uow.Equipment.Delete(equipment);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync() // получить все оборудование
        {
            return await _uow.Equipment.GetAllAsync();
        }

        public async Task<Equipment> GetEquipmentByIdAsync(int id)
        {
            var list = await _uow.Equipment.FindAllAsync(e => e.EquipmentId == id);
            return list.FirstOrDefault();
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            //на всякий случай что бы не было такого же как и с невозможностью отредактировать одного и того же челоека
            var existingEquipment = await _uow.Equipment.GetByIdAsync(equipment.EquipmentId);

            if(existingEquipment != null)
            {
                existingEquipment.Name = equipment.Name;
                existingEquipment.Model = equipment.Model;
                existingEquipment.SerialNumber = equipment.SerialNumber;
                existingEquipment.ClassName = equipment.ClassName;
                existingEquipment.Service = equipment.Service;
                existingEquipment.Formular = equipment.Formular;
                existingEquipment.FormularDate = equipment.FormularDate;
                existingEquipment.Year = equipment.Year;
                existingEquipment.InventoryNumber = equipment.InventoryNumber;
                existingEquipment.BoxNumber = equipment.BoxNumber;
                existingEquipment.Amount = equipment.Amount;
                existingEquipment.CountryOfOrigin = equipment.CountryOfOrigin;
                existingEquipment.Weight = equipment.Weight;
                existingEquipment.TNVEDCode = equipment.TNVEDCode;

                await _uow.SaveChangesAsync();
            }
        }
    }
}
