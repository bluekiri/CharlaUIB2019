using StarWarsAPI.Domain.Models.VehiclesAggregate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StarWarsAPI.Infrastructure.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly string _textFile;
        private readonly string _path;

        public VehiclesRepository()
        {
            _path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "./Infrastructure/StubData/Data/Vehicles.json");
            _textFile = File.ReadAllText(_path);
        }

        public bool Create(VehicleModel entity)
        {
            if (entity == default) return false;
            ICollection<VehicleModel> vehicles = ReadAll();
            VehicleModel model = vehicles.FirstOrDefault(x => x.Id == entity.Id);
            if (model == default)
            {
                vehicles.Add(entity);
                File.WriteAllText(_path, JsonSerializer.Serialize(vehicles));
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            ICollection<VehicleModel> planets = ReadAll();
            VehicleModel model = planets.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = planets.Remove(model);
                if (remove)
                {
                    File.WriteAllText(_path, JsonSerializer.Serialize(planets));
                    return true;
                }
            }
            return false;
        }


        public VehicleModel Read(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(int id, VehicleModel entity)
        {
            if (entity is null) return false;

            ICollection<VehicleModel> planets = ReadAll();
            VehicleModel model = planets.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = planets.Remove(model);
                if (remove)
                {
                    planets.Add(entity);
                    File.WriteAllText(_path, JsonSerializer.Serialize(planets));
                    return true;
                }
            }
            return false;
        }

        public ICollection<VehicleModel> ReadAll()
        {
            return JsonSerializer.Deserialize<ICollection<VehicleModel>>(_textFile);
        }
    }
}
