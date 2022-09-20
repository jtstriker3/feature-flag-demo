using feature_flag_demo.Models;

namespace feature_flag_demo
{
    public interface IShipmentRepo
    {
        Task<Shipment?> GetShipmentAsync(string id);
    }
}