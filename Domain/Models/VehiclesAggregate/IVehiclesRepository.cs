using System.Collections.Generic;

namespace StarWarsAPI.Domain.Models.VehiclesAggregate
{
    public interface IVehiclesRepository
    {
        bool Create(VehicleModel entity);
        bool Delete(int id);
        VehicleModel Read(int id);
        bool Update(int id, VehicleModel entity);
        ICollection<VehicleModel> ReadAll();
    }
}