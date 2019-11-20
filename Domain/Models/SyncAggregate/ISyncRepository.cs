using System.Threading.Tasks;

namespace StarWarsAPI.Domain.Models.SyncAggregate
{
    public interface ISyncRepository
    {
        Task<bool> FillDataText(int? film, bool canIsertedCharacters, bool canIsertedPlanets, bool canIsertedStarships, bool canIsertedSpecies, bool canIsertedVehicles);
    }
}