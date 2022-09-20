using System;
namespace feature_flag_demo.Models
{
    public class Shipment
    {
        public string? ShipmentId { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public bool IsDeleted { get; set; }
    }
}

