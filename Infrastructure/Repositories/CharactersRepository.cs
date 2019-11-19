using StarWarsAPI.Domain.Models.CharactersAggregate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StarWarsAPI.Infrastructure.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly string _path;
        private readonly string _textFile;

        public CharactersRepository()
        {
            _path = "./StubData/Data/Characters.json";
            _textFile = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), _path));
        }

        public bool Create(CharacterModel entity)
        {
            if (entity == default) return false;
            ICollection<CharacterModel> characters = ReadAll();
            CharacterModel model = characters.FirstOrDefault(x => x.Id == entity.Id);
            if (model == default)
            {
                characters.Add(entity);
                File.WriteAllText(_path, JsonSerializer.Serialize(characters));
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            ICollection<CharacterModel> characters = ReadAll();
            CharacterModel model = characters.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = characters.Remove(model);
                if (remove)
                {
                    File.WriteAllText(_path, JsonSerializer.Serialize(characters));
                    return true;
                }
            }
            return false;
        }


        public CharacterModel Read(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(int id, CharacterModel entity)
        {
            if (entity is null) return false;

            ICollection<CharacterModel> characters = ReadAll();
            CharacterModel model = characters.FirstOrDefault(x => x.Id == id);
            if (model != default)
            {
                bool remove = characters.Remove(model);
                if (remove)
                {
                    characters.Add(entity);
                    File.WriteAllText(_path, JsonSerializer.Serialize(characters));
                    return true;
                }
            }
            return false;
        }

        public ICollection<CharacterModel> ReadAll()
        {
            return JsonSerializer.Deserialize<ICollection<CharacterModel>>(_textFile);
        }
    }
}
