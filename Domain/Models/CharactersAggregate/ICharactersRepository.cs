using System.Collections.Generic;

namespace StarWarsAPI.Domain.Models.CharactersAggregate
{
    public interface ICharactersRepository
    {
        bool Create(CharacterModel entity);
        bool Delete(int id);
        CharacterModel Read(int id);
        bool Update(int id, CharacterModel entity);
        ICollection<CharacterModel> ReadAll();
    }
}