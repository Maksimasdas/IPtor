using IPtor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; set; }          // Первичный ключ
        public string Name { get; set; }           // ФИО
        public ServiceType Service { get; set; }   // Служба
        public string Position { get; set; }       // Должность
        public string Rank { get; set; }           // Звание
        public string UndPosition { get; set; }    // Сокращённое название должности
    }
}
