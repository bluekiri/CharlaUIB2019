using System.Collections.Generic;

namespace StarWarsAPI.Domain.Models.StarshipsAggregate
{
    public interface IStarshipsRepository
    {
        bool Create(StarshipsModel entity);
        bool Delete(int id);
        StarshipsModel Read(int id);
        bool Update(int id, StarshipsModel entity);
        ICollection<StarshipsModel> ReadAll();
    }
}