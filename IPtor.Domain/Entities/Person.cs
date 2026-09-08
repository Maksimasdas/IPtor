using IPtor.Domain.Enums;

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
        public bool IsActive { get; set; } = true;
    }
}
