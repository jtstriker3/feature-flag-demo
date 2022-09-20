using System;
using feature_flag_demo.Models;

namespace feature_flag_demo
{
    public class ShipmentRepoV2 : IShipmentRepo
    {
        private readonly List<Shipment> _shipments;
        private readonly FeatureExecutor featureExecutor;

        public ShipmentRepoV2(FeatureExecutor featureExecutor)
        {
            _shipments = new List<Shipment>()
            {
                new Shipment()
                {
                    ShipmentId = "Shipment1",
                    Origin = "1234 Packers Street, Green Bay WI",
                    Destination = "1234 bears Street, Chicago IL",
                    IsDeleted = true
                },
                new Shipment()
                {
                    ShipmentId = "Shipment2",
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

            featureExecutor.Execute("Add4thShipment",
                impls: (
                    true.ToString(),
                    () =>
                        {
                            _shipments.Add(new Shipment()
                            {
                                ShipmentId = "Shipment4",
                                Origin = "1234 Packers Street, Green Bay WI",
                                Destination = "1234 bears Street, Chicago IL"

                            });
                        }
            )
            );

            this.featureExecutor = featureExecutor;
        }



        public async Task<Shipment?> GetShipmentAsync(string id)
        {
            // Typical Shipment logic would go here
            return await Task.FromResult(
                _shipments
                .FirstOrDefault(_ =>
                    _.ShipmentId == id
                    && !_.IsDeleted
                )
            );
        }

        public int ShipmentCount => _shipments.Count();
    }
}

