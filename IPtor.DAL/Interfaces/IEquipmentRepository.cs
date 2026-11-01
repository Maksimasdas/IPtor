using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.DAL.Interfaces
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        Task<IEnumerable<Equipment>> GetEquipmentWithDetailsAsync();
        Task<Equipment> GetEquipmentWithFullInfoAsync(int id);
        Task<IEnumerable<Equipment>> GetEquipmentByServiceAsync(ServiceType service);
    }
}
