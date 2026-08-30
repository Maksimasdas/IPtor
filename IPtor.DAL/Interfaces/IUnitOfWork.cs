using IPtor.Domain.Entities;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPtor.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository Cars {  get; }
        IRepository<Battery> Batteries { get; }
        IRepository<Tire> Tires { get; }
        IRepository<Defects> Defects { get; }
        IRepository<DefectsTO> DefectsTOs { get; }
        //IPersonRepository Persons { get; }
        IRepository<Person> Persons { get; }
        IRepository<RepairRequest> RepairRequests { get; }

        Task<int> SaveChangesAsync();
    }
}
