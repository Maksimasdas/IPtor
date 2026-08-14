using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.DAL.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllPersonsWithNoTrackingAsync();
        Task<Person> GetPersonWithNoTrackingAsync(int id);
    }
}
