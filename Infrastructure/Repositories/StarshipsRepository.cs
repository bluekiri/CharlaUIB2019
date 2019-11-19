using StarWarsAPI.Domain.Models.StarshipsAggregate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StarWarsAPI.Infrastructure.Repositories
{
    public class StarshipsRepository : IStarshipsRepository
    {
        private readonly string _path;
        private readonly string _textFile;

        public StarshipsRepository()
        {
            _path = "./StubData/Data/Requests/Starships.json";
            _textFile = File.ReadAllText(_path);
        }

        public bool Create(StarshipsModel entity)
        {
            if (entity == default) return false;
            ICollection<StarshipsModel> starships = ReadAll();
            StarshipsModel model = starships.FirstOrDefault(x => x.Id == entity.Id);
            if (model == default)
            {
                starships.Add(entity);
                File.WriteAllText(_path, JsonSerializer.Serialize(starships));
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            ICollection<StarshipsModel> starships = ReadAll();
            StarshipsModel model = starships.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = starships.Remove(model);
                if (remove)
                {
                    File.WriteAllText(_path, JsonSerializer.Serialize(starships));
                    return true;
                }
            }
            return false;
        }


        public StarshipsModel Read(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(int id, StarshipsModel entity)
        {
            if (entity is null) return false;

            ICollection<StarshipsModel> starships = ReadAll();
            StarshipsModel model = starships.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = starships.Remove(model);
                if (remove)
                {
                    starships.Add(entity);
                    File.WriteAllText(_path, JsonSerializer.Serialize(starships));
                    return true;
                }
            }
            return false;
        }

        public ICollection<StarshipsModel> ReadAll()
        {
            return JsonSerializer.Deserialize<ICollection<StarshipsModel>>(_textFile);
        }
    }
}
