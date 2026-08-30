using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Models
{
    public class TOAktData
    {
        public Car Car { get; set; }
        public Person SheffPassService { get; set; }    // Сдающий (старший службы)
        public Person ResponcePerson { get; set; }       // Ответственный со стороны сдающего
        public Person Driver { get; set; }               // Водитель
        public DateTime TransferDate { get; set; }       // дата акта
        public DefectsTO DefectsTO { get; set; }         // информация к акту
        public int KmSpeedometrStart { get; set; }       // Показания спидометра
        public float MhSpeedStart { get; set; }          // Моточасы по спидометру

        public TOAktData()
        {
            TransferDate = DateTime.Now;
        }
    }
}
