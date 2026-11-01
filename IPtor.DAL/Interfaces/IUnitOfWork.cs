using IPtor.Domain.Entities;

namespace IPtor.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository Cars { get; }
        IRepository<Battery> Batteries { get; }
        IRepository<Tire> Tires { get; }
        IRepository<Defects> Defects { get; }
        IRepository<DefectsTO> DefectsTOs { get; }
        //IPersonRepository Persons { get; }
        IRepository<Person> Persons { get; }
        IRepository<RepairRequest> RepairRequests { get; }
        //IRepository<Equipment> Equipment { get; }
        IEquipmentRepository Equipment { get; }

        Task<int> SaveChangesAsync();
    }
}
