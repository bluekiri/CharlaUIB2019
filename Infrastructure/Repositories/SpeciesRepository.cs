using StarWarsAPI.Domain.Models.SpeciesAggregate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StarWarsAPI.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly string _path;
        private readonly string _textFile;

        public SpeciesRepository()
        {
            _path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "./Infrastructure/StubData/Data/Species.json");
            _textFile = File.ReadAllText(_path);
        }

        public bool Create(SpeciesModel entity)
        {
            if (entity == default) return false;
            ICollection<SpeciesModel> species = ReadAll();
            SpeciesModel model = species.FirstOrDefault(x => x.Id == entity.Id);
            if (model == default)
            {
                species.Add(entity);
                File.WriteAllText(_path, JsonSerializer.Serialize(species));
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            ICollection<SpeciesModel> species = ReadAll();
            SpeciesModel model = species.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = species.Remove(model);
                if (remove)
                {
                    File.WriteAllText(_path, JsonSerializer.Serialize(species));
                    return true;
                }
            }
            return false;
        }


        public SpeciesModel Read(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(int id, SpeciesModel entity)
        {
            if (entity is null) return false;

            ICollection<SpeciesModel> species = ReadAll();
            SpeciesModel model = species.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = species.Remove(model);
                if (remove)
                {
                    species.Add(entity);
                    File.WriteAllText(_path, JsonSerializer.Serialize(species));
                    return true;
                }
            }
            return false;
        }

        public ICollection<SpeciesModel> ReadAll()
        {
            return JsonSerializer.Deserialize<ICollection<SpeciesModel>>(_textFile);
        }
    }
}
