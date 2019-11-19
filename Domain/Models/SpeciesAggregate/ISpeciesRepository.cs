using System.Collections.Generic;

namespace StarWarsAPI.Domain.Models.SpeciesAggregate
{
    public interface ISpeciesRepository
    {
        bool Create(SpeciesModel entity);
        bool Delete(int id);
        SpeciesModel Read(int id);
        bool Update(int id, SpeciesModel entity);
        ICollection<SpeciesModel> ReadAll();
    }
}