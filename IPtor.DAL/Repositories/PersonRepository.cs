using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.DAL.Repositories
{
    public class PersonRepository : EntityRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Person>> GetAllPersonsWithNoTrackingAsync()
        {
            return await _context.Persons
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Person> GetPersonWithNoTrackingAsync(int id)
        {
            return await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonId == id);
        }
    }
}
