using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Domain.Entities
{
    public class Tire
    {
        public int TireId { get; set; }
        public float Width { get; set; } //165
        public float? Profile { get; set; } //65
        public float Diameter { get; set; } //15
        public string Brand { get; set; } //марка шины 
        public string Model { get; set; } //модель шины
        public DateTime InstallationDate { get; set; }

        //навигационное свойство 
        public int CarId { get; set; }
        public Car Car { get; set; }

    }
}
