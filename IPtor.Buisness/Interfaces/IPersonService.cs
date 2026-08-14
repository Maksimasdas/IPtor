using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(int id);
    }
}
