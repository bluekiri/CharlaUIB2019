using System.Collections.Generic;

namespace StarWarsAPI.Domain.Models.PlanetsAggregate
{
    public interface IPlanetsRepository
    {
        bool Create(PlanetsModel entity);
        bool Delete(int id);
        PlanetsModel Read(int id);
        bool Update(int id, PlanetsModel entity);
        ICollection<PlanetsModel> ReadAll();
    }
}