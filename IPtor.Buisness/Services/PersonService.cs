using DocumentFormat.OpenXml.Wordprocessing;
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
            person.IsActive = true;
            await _uow.Persons.AddAsync(person);
            await _uow.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _uow.Persons.GetByIdAsync(id);
            if (person == null) return;

            //проверяем на участие в актах дефектовки
            var defectsWithPerson = await _uow.Defects.FindAllAsync(d =>
                d.SheffPassServiceId == id ||
                d.ResponcePersonId == id ||
                d.DriverId == id ||
                d.AcceptUtoPersonId == id ||
                d.AdditionalPersonId == id);

            //проверяем на участие в актах ТО
            var defectsTOWithPerson = await _uow.DefectsTOs.FindAllAsync(d =>
                d.SheffPassServiceId == id ||
                d.ResponcePersonId == id ||
                d.DriverId == id);

            if (defectsWithPerson.Any() || defectsTOWithPerson.Any())
            {
                //мягкое удаление, по скольку есть акты
                person.IsActive = false;
                _uow.Persons.Update(person);
                await _uow.SaveChangesAsync();
            }
            else
            {
                // Не задействован — физическое удаление
                _uow.Persons.Delete(person);
                await _uow.SaveChangesAsync();
            }
        }

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
                existingPerson.IsActive = person.IsActive;

                await _uow.SaveChangesAsync();
            }
        }
        //получить всех рабтников
        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            //return await _uow.Persons.GetAllPersonsWithNoTrackingAsync();
            //return await _uow.Persons.GetAllAsync();
            //возвращаем только активных сотрудников
            return await _uow.Persons.FindAllAsync(p => p.IsActive);
        }
        //получить работника по ИД
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            //return await _uow.Persons.GetPersonWithNoTrackingAsync(id);
            //return await _uow.Persons.GetByIdAsync(id);
            var persons = await _uow.Persons.FindAllAsync(p => p.PersonId == id);
            return persons.FirstOrDefault();
        }
    }
}
