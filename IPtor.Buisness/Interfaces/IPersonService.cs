using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync(); // получить всех работников
        Task<Person> GetPersonByIdAsync(int id); // получить работника по ИД
        Task AddPersonAsync(Person person); // добавить работника
        Task UpdatePersonAsync(Person person); // обновить работника
        Task DeletePersonAsync(int id); // удалить работника
    }
}
