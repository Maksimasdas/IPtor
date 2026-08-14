using System;

namespace IPtor.Domain.Entities
{
    public class RepairRequest
    {
        public int RepairRequestId { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Ожидает ремонта";

        public Car Car { get; set; }
    }
}