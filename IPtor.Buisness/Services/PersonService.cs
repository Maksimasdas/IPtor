using IPtor.Buisness.Interfaces;
using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _uow;
        public PersonService (IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task AddPersonAsync(Person person)
        {
            await _uow.Persons.AddAsync(person);
            await _uow.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _uow.Persons.GetByIdAsync(id);
            if(person != null)
            {
                _uow.Persons.Delete(person);
                await _uow.SaveChangesAsync();
            }
        }

        //public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        //{
        //   return await _uow.Persons.GetAllAsync();
        //}

        //public async Task<Person> GetPersonByIdAsync(int id)
        //{
        //    return await _uow.Persons.GetByIdAsync(id);   
        //}

        public async Task UpdatePersonAsync(Person person)
        {
            _uow.Persons.Update(person);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _uow.Persons.GetAllPersonsWithNoTrackingAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _uow.Persons.GetPersonWithNoTrackingAsync(id);
        }
    }
}
