using System;
using feature_flag_demo.Models;

namespace feature_flag_demo
{
    public class ShipmentRepoV1 : IShipmentRepo
    {
        private readonly List<Shipment> _shipments;

        public ShipmentRepoV1()
        {
            _shipments = new List<Shipment>()
            {
                new Shipment()
                {
                    ShipmentId = "Shipment1",
                    Origin = "1234 Packers Street, Green Bay WI",
                    Destination = "1234 bears Street, Chicago IL"
                },
                new Shipment()
                {
                    ShipmentId = "Shipment3",
                    Origin = "1234 Packers Street, Green Bay WI",
                    Destination = "1234 bears Street, Chicago IL"
                },
                new Shipment()
                {
                    ShipmentId = "Shipment3",
                    Origin = "1234 Packers Street, Green Bay WI",
                    Destination = "1234 bears Street, Chicago IL"
                }
            };
        }



        public async Task<Shipment?> GetShipmentAsync(string id)
        {
            // Typical Shipment logic would go here
            return await Task.FromResult(
                _shipments
                .FirstOrDefault(_ =>
                    _.ShipmentId == id
                )
            );
        }
    }
}

