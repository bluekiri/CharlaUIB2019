using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StarWarsAPI.Domain.Models.SyncAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsAPI.Infrastructure.Service
{
    /// <summary>
    /// QueuedSyncService
    /// </summary>
    public class QueuedSyncService : BackgroundService
    {
        private readonly ILogger<QueuedSyncService> _logger;
        private readonly IBackgroundTaskQueue _taskQueue;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="taskQueue"></param>
        public QueuedSyncService(ILogger<QueuedSyncService> logger, IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            _taskQueue = taskQueue;
        }

        /// <summary>
        /// Execute Task Async 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                Func<CancellationToken, Task> workItem = await _taskQueue.DequeueAsync(cancellationToken);

                try
                {
                    await workItem(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred executing {nameof(workItem)}.");
                }
            }
        }
    }
}
