using System;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsAPI.Domain.Models.SyncAggregate
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);
    }
}
