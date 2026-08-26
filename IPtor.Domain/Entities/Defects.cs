using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Domain.Entities
{
    public class Defects
    {
        public int DefectsId { get; set; }//первичный ключ
        public string? Staff { get; set; }
        public string? EngineDefect { get; set; }
        public string? Electro { get; set; }
        public string? Clutch { get; set; }
        public string? Kpp { get; set; }
        public string? Steering { get; set; }
        public string? FrontGear { get; set; }
        public string? BackGear { get; set; } 
        public string? Frame { get; set; }
        public string? Body { get; set; }
        public string? SpecEq { get; set; }
        public string? ExternalCond { get; set; }
        public string? NumberWheel { get; set; }
        public float? FuelAvail { get; set; }
        public string? Conclusion { get; set; }
        //Дополнительно создано для истории актов
        public DateTime ActDate { get; set; } = DateTime.Now;

        //навигационное свойство
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
