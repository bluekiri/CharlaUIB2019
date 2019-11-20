using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWarsAPI.Domain.Models.SyncAggregate;
using System;
using System.Net;

namespace StarWarsAPI.Controllers
{
    /// <summary>
    /// VehiclesController
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly ISyncRepository _repository;
        private readonly ILogger<SyncController> _logger;
        private readonly IBackgroundTaskQueue _taskQueue;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        /// <param name="taskQueue"></param>
        public SyncController(ISyncRepository service, ILogger<SyncController> logger, IBackgroundTaskQueue taskQueue)
        {
            _repository = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taskQueue = taskQueue ?? throw new ArgumentNullException(nameof(taskQueue));
        }

        /// <summary>
        /// Sync data on documents
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<bool> SyncData(int? film = null, bool canIsertedCharacters = false, bool canIsertedPlanets = false, bool canIsertedStarships = false, bool canIsertedSpecies = false, bool canIsertedVehicles = false)
        {
            try
            {
                _taskQueue.QueueBackgroundWorkItem(async cancellationToken =>
                {
                    await _repository.FillDataText(film, canIsertedCharacters, canIsertedPlanets, canIsertedStarships, canIsertedSpecies, canIsertedVehicles);
                });

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Controller exception sync data");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

    }
}
