using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWarsAPI.Domain.Models.SyncAggregate;
using System;
using System.Net;
using System.Threading.Tasks;

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

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public SyncController(ISyncRepository service, ILogger<SyncController> logger)
        {
            _repository = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Sync data on documents
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<bool>> SyncData()
        {
            try
            {
                return Ok(await _repository.FillDataText());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Controller exception sync data");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

    }
}
