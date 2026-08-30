using IPtor.DAL.Interfaces;
using IPtor.DAL.Repositories;
using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ICarRepository _cars;
        private IRepository<Battery> _batteries;
        private IRepository<Tire> _tires;
        private IRepository<Defects> _defects;
        private IRepository<DefectsTO> _defectsTOs;
        private IRepository<Person> _persons;
        //private IPersonRepository _persons;
        private IRepository<RepairRequest> _repairRequests;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICarRepository Cars => _cars ??= new CarRepository(_context);

        public IRepository<Battery> Batteries => _batteries ??= new EntityRepository<Battery>(_context);

        public IRepository<Tire> Tires => _tires ??= new EntityRepository<Tire>(_context);

        public IRepository<Defects> Defects => _defects ??= new EntityRepository<Defects>(_context);

        public IRepository<DefectsTO> DefectsTOs => _defectsTOs ??= new EntityRepository<DefectsTO>(_context);

        public IRepository<Person> Persons => _persons ??= new EntityRepository<Person>(_context);
        //public IPersonRepository Persons => _persons ??= new PersonRepository(_context);
        public IRepository<RepairRequest> RepairRequests => _repairRequests ??= new EntityRepository<RepairRequest>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
