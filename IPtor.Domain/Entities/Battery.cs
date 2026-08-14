using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Domain.Entities
{
    public class Battery
    {
        public int BatteryId { get; set; } //ИД батареи
        public string Specifications { get; set; } //типа 6СТ-90
        public string StartingCurrent {  get; set; } //пусковой ток
        public string Polarity { get; set; } //полярность
        public string BodyType { get; set; } //тип корпуса
        public DateTime InstallationDate { get; set; } //дата установки

        //навигационное свойство
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
