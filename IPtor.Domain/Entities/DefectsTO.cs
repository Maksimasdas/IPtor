using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Domain.Entities
{
    public class DefectsTO
    {
        public int DefectsTOId { get; set; } //первичный ключ
        //тех состояния
        public string? Body { get; set; }
        public string? HeadBody { get; set; }
        public string? Engine { get; set; }
        public string? KPP { get; set; }
        public string? Rul { get; set; }
        public string? FrontGear { get; set; }
        public string? BackGear { get; set; }
        public string? Electro { get; set; }
        public string? PTV { get; set; }
        public string? Water { get; set; }
        public string? GeneralView { get; set; }
        public string? Spec { get; set; }
        public string? Break { get; set; }
        public string? Other { get; set; }

        //укомплектовааность

        public string? PuskRuk { get; set; }
        public string? Domkrat { get; set; }
        public string? Nasos { get; set; }
        public string? SHpric { get; set; }
        public string? Molotok { get; set; }
        public string? Ploskogubci { get; set; }
        public string? Kluchi { get; set; }
        public string? Cluchi2 { get; set; }
        public string? Cluchi3 { get; set; }
        public string? Cluchi4 { get; set; }
        public string? Chehli { get; set; }
        public string? Aptechka { get; set; }
        public string? Znak { get; set; }
        public string? Disk { get; set; }
        public string? Ognetush { get; set; }
        public string? Radio { get; set; }
        public string? Protivougon { get; set; }
        public string? Gromkogov { get; set; }
        public string? Protivotuman { get; set; }
        public string? Mayak { get; set; }
        public string? Fara { get; set; }
        public string? Prochee { get; set; }
        public float Fuel { get; set; }
        public string? Zakl { get; set; }
        public string? Pred { get; set; }

        //навигационное свойство 
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
