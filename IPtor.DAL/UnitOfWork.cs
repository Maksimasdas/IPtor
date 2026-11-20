using IPtor.DAL.Interfaces;
using IPtor.DAL.Repositories;
using IPtor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace IPtor.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private AppDbContext _context;

        private ICarRepository _cars;
        private IRepository<Battery> _batteries;
        private IRepository<Tire> _tires;
        private IRepository<Defects> _defects;
        private IRepository<DefectsTO> _defectsTOs;
        private IRepository<Person> _persons;
        private IRepository<RepairRequest> _repairRequests;
        private IEquipmentRepository _equipment;

        public UnitOfWork(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private AppDbContext Context => _context ??= _contextFactory.CreateDbContext();

        public ICarRepository Cars => _cars ??= new CarRepository(Context);
        public IRepository<Battery> Batteries => _batteries ??= new EntityRepository<Battery>(Context);
        public IRepository<Tire> Tires => _tires ??= new EntityRepository<Tire>(Context);
        public IRepository<Defects> Defects => _defects ??= new EntityRepository<Defects>(Context);
        public IRepository<DefectsTO> DefectsTOs => _defectsTOs ??= new EntityRepository<DefectsTO>(Context);
        public IRepository<Person> Persons => _persons ??= new EntityRepository<Person>(Context);
        public IRepository<RepairRequest> RepairRequests => _repairRequests ??= new EntityRepository<RepairRequest>(Context);
        public IEquipmentRepository Equipment => _equipment ??= new EquipmentRepository(Context);

        public async Task WaitAsync() => await _semaphore.WaitAsync();
        public void Release() => _semaphore.Release();

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
        }
    }
}