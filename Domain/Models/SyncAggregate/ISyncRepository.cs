using System.Threading.Tasks;

namespace StarWarsAPI.Domain.Models.SyncAggregate
{
    public interface ISyncRepository
    {
        Task<bool> FillDataText();
    }
}