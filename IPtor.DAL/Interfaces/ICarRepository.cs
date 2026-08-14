using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPtor.DAL.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetCarsWithDetailsAsync();
        Task<Car> GetCarWithFullInfoAsync(int id);
        Task<IEnumerable<Car>> GetCarsByServiceAsync(ServiceType service);
    }
}
