using IPtor.Buisness.Interfaces;
using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _uow;
        public CarService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //добавить машину
        public async Task AddCarAsync(Car car)
        {
            await _uow.Cars.AddAsync(car);
            await _uow.SaveChangesAsync();
        }
        //удалить машшину
        public async Task DeleteCarAsync(int id)
        {
            var car = await _uow.Cars.GetByIdAsync(id);
            if (car != null)
            {
                _uow.Cars.Delete(car);
                await _uow.SaveChangesAsync();
            }
        }
        //получить все машины
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _uow.Cars.GetAllAsync();
        }
        //получить машины определенной службы
        public async Task<IEnumerable<Car>> GetCarsByServiceAsync(ServiceType service)
        {
            return await _uow.Cars.GetCarsByServiceAsync(service);
        }
        //получить машину с полной информацией
        public async Task<IEnumerable<Car>> GetCarsWithDetailsAsync()
        {
            return await _uow.Cars.GetCarsWithDetailsAsync();
        }
        //получить машину с полной информацией
        public async Task<Car> GetCarWithFullInfoAsync(int id)
        {
            return await _uow.Cars.GetCarWithFullInfoAsync(id);
        }
        //обновить машину
        public async Task UpdateCarAsync(Car car)
        {
            //исправление глюка с невозможностью отредактировать дважды одну и ту же машину
            var existingCar = await _uow.Cars.GetByIdAsync(car.CarId);

            if (existingCar != null)
            {
                existingCar.Name = car.Name;
                existingCar.Model = car.Model;
                existingCar.Number = car.Number;
                existingCar.ClassName = car.ClassName;
                existingCar.Service = car.Service;
                existingCar.Vin = car.Vin;
                existingCar.Certificate = car.Certificate;
                existingCar.EngineNumber = car.EngineNumber;
                existingCar.Formular = car.Formular;
                existingCar.FormularDate = car.FormularDate;
                existingCar.Year = car.Year;
                existingCar.InventoryNumber = car.InventoryNumber;
            
                await _uow.SaveChangesAsync();
            }
        }
        //добавить АКБ
        public async Task AddBatteryAsync(Battery battery)
        {
            await _uow.Batteries.AddAsync(battery);
            await _uow.SaveChangesAsync();
        }
        //добавить шину
        public async Task AddTireAsync(Tire tire)
        {
            await _uow.Tires.AddAsync(tire);
            await _uow.SaveChangesAsync();
        }
        //удалить АКБ
        public async Task DeleteBatteryAsync(int id)
        {
            var battery = await _uow.Batteries.GetByIdAsync(id);
            if (battery != null)
            {
                _uow.Batteries.Delete(battery);
                await _uow.SaveChangesAsync();
            }
        }
        //удалить шину
        public async Task DeleteTireAsync(int id)
        {
            var tire = await _uow.Tires.GetByIdAsync(id);
            if (tire != null)
            {
                _uow.Tires.Delete(tire);
                await _uow.SaveChangesAsync();
            }
        }
        //добавлено для созранения истории актов 2 метода
        public async Task AddDefectsAsync(Defects defects)
        {
            await _uow.Defects.AddAsync(defects);
            await _uow.SaveChangesAsync();
        }
        public async Task AddDefectsTOAsync(DefectsTO defectsTO)
        {
            await _uow.DefectsTOs.AddAsync(defectsTO);
            await _uow.SaveChangesAsync();
        }
        
        //методы для обновления шин и акб 2 метода
        public async Task UpdateBatteryAsync(Battery battery)
        {
            var existing = await _uow.Batteries.GetByIdAsync(battery.BatteryId);
            if(existing != null)
            {
                existing.Specifications = battery.Specifications;
                existing.StartingCurrent = battery.StartingCurrent;
                existing.Polarity = battery.Polarity;
                existing.BodyType = battery.BodyType;
                existing.InstallationDate = battery.InstallationDate;
                await _uow.SaveChangesAsync();
            }
        }

        public async Task UpdateTireAsync(Tire tire)
        {
            var existing = await _uow.Tires.GetByIdAsync(tire.TireId);
            if(existing != null)
            {
                existing.Width = tire.Width;
                existing.Profile = tire.Profile;
                existing.Diameter = tire.Diameter;
                existing.Brand = tire.Brand;
                existing.Model = tire.Model;
                existing.InstallationDate = tire.InstallationDate;
                await _uow.SaveChangesAsync();
            }
        }
        
        public async Task SetRepairStatusAsync(int carId, bool isUnderRepair, string? reason)
        {
            var car = await _uow.Cars.GetByIdAsync(carId);
            if(car != null)
            {
                car.IsUnderRepair = isUnderRepair;
                car.RepairReason = isUnderRepair ? reason : null;
                car.RepairStartDate = isUnderRepair ? DateTime.Now : null;
                await _uow.SaveChangesAsync();
            }
        }
    }
}
