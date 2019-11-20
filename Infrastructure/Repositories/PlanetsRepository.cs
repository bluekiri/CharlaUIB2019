using StarWarsAPI.Domain.Models.PlanetsAggregate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StarWarsAPI.Infrastructure.Repositories
{
    public class PlanetsRepository : IPlanetsRepository
    {
        private readonly string _path;
        private readonly string _textFile;

        public PlanetsRepository()
        {
            _path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "./Infrastructure/StubData/Data/Planets.json");
            _textFile = File.ReadAllText(_path);
        }

        public bool Create(PlanetsModel entity)
        {
            if (entity == default) return false;
            ICollection<PlanetsModel> planets = ReadAll();
            PlanetsModel model = planets.FirstOrDefault(x => x.Id == entity.Id);
            if (model == default)
            {
                planets.Add(entity);
                File.WriteAllText(_path, JsonSerializer.Serialize(planets));
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            ICollection<PlanetsModel> planets = ReadAll();
            PlanetsModel model = planets.FirstOrDefault(x => x.Id == id);
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


        public PlanetsModel Read(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(int id, PlanetsModel entity)
        {
            if (entity is null) return false;

            ICollection<PlanetsModel> planets = ReadAll();
            PlanetsModel model = planets.FirstOrDefault(x => x.Id == id);
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

        public ICollection<PlanetsModel> ReadAll()
        {
            return JsonSerializer.Deserialize<ICollection<PlanetsModel>>(_textFile);
        }

    }
}
