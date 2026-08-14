using IPtor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace IPtor.Buisness.Models
{
    public class TransferActData
    {
        public Car Car { get; set; }
        public Person SheffPassService { get; set; }      // Сдающий (старший службы)
        public Person ResponcePerson { get; set; }         // Ответственный со стороны сдающего
        public Person Driver { get; set; }                 // Водитель
        public Person AcceptUtoPerson { get; set; }        // Принимающий (от чьего имени акт)
        public Person AdditionalPerson { get; set; }       // Представитель УТО (физ. приёмщик)

        public DateTime TransferDate { get; set; }
        public string AdditionalNotes { get; set; }

        // Дефекты — отдельным объектом, не вся таблица
        public Defects Defects { get; set; }

        // Километры (целые)
        public int KmSpeedStart { get; set; }
        public int KmGeneral { get; set; }
        public int KmSpeedKarRepair { get; set; }
        public int KmGeneralKapRepair { get; set; }
        public int KmSpeedTO { get; set; }
        public int KmGeneralTO { get; set; }
        public int AvarageRepair { get; set; }
        public int KapRepair { get; set; }

        // Моточасы (дробные)
        public float MhSpeedStart { get; set; }
        public float MhSpeedKapRepair { get; set; }
        public float MhSpeedTO { get; set; }

        // Расчётные
        public float KmAfterRepair { get; set; }
        public float MhAfterRepair { get; set; }
        public float KmAfterTO { get; set; }
        public float MhAfterTO { get; set; }

        public TransferActData()
        {
            Defects = new Defects();
            TransferDate = DateTime.Now;
        }
    }
}
