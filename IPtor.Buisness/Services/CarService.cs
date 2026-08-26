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
        public async Task AddCarAsync(Car car)
        {
            await _uow.Cars.AddAsync(car);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _uow.Cars.GetByIdAsync(id);
            if (car != null)
            {
                _uow.Cars.Delete(car);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _uow.Cars.GetAllAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsByServiceAsync(ServiceType service)
        {
            return await _uow.Cars.GetCarsByServiceAsync(service);
        }

        public async Task<IEnumerable<Car>> GetCarsWithDetailsAsync()
        {
            return await _uow.Cars.GetCarsWithDetailsAsync();
        }

        public async Task<Car> GetCarWithFullInfoAsync(int id)
        {
            return await _uow.Cars.GetCarWithFullInfoAsync(id);
        }

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



            //_uow.Cars.Update(car);
            //await _uow.SaveChangesAsync();
        }
        public async Task AddBatteryAsync(Battery battery)
        {
            await _uow.Batteries.AddAsync(battery);
            await _uow.SaveChangesAsync();
        }

        public async Task AddTireAsync(Tire tire)
        {
            await _uow.Tires.AddAsync(tire);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteBatteryAsync(int id)
        {
            var battery = await _uow.Batteries.GetByIdAsync(id);
            if (battery != null)
            {
                _uow.Batteries.Delete(battery);
                await _uow.SaveChangesAsync();
            }
        }

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
    }
}
