using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();                          // Все машины без связей
        Task<IEnumerable<Car>> GetCarsWithDetailsAsync();                  // Машины + батареи + шины
        Task<Car> GetCarWithFullInfoAsync(int id);                         // Одна машина + всё
        Task<IEnumerable<Car>> GetCarsByServiceAsync(ServiceType service); // Фильтр по службе
        Task AddCarAsync(Car car);                                         // Добавить машину
        Task UpdateCarAsync(Car car);                                      // Обновить машину
        Task DeleteCarAsync(int id);                                       // Удалить машину
        Task AddBatteryAsync(Battery battery);                             // добавить акб
        Task AddTireAsync(Tire tire);                                      // доабвить шину
        Task DeleteBatteryAsync(int id);                                   // удалить акб
        Task DeleteTireAsync(int id);                                      // удалить шину
        Task AddDefectsAsync(Defects defects);                             // сохранить историю акта приемки
        Task AddDefectsTOAsync(DefectsTO defectsTO);                       // сохранить историю акта ТО
        Task UpdateBatteryAsync(Battery battery);                          // обновить акб
        Task UpdateTireAsync(Tire tire);                                   // обновить шину
    }
}
