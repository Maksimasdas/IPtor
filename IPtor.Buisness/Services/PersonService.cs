using IPtor.Buisness.Interfaces;
using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Xsl;

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
            //исправление глюка с невозможностью отредактировать дважды одного и того же человека
            var existingPerson = await _uow.Persons.GetByIdAsync(person.PersonId);

            if(existingPerson != null)
            {
                existingPerson.Name = person.Name;
                existingPerson.Position = person.Position;
                existingPerson.Rank = person.Rank;
                existingPerson.UndPosition = person.UndPosition;
                existingPerson.Service = person.Service;

                await _uow.SaveChangesAsync();
            }
            //_uow.Persons.Update(person);
            //await _uow.SaveChangesAsync();
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
