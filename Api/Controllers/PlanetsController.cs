using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWarsAPI.Domain.Models.PlanetsAggregate;
using System;
using System.Collections.Generic;
using System.Net;

namespace StarWarsAPI.Controllers
{
    /// <summary>
    /// PlanetsController
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetsRepository _repository;
        private readonly ILogger<PlanetsController> _logger;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public PlanetsController(IPlanetsRepository service, ILogger<PlanetsController> logger)
        {
            _repository = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Create Planet on the document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> Create(PlanetsModel input)
        {
            try
            {
                return Ok(_repository.Create(input));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Controller exception on Create(T input)");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Get Planet on the document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<PlanetsModel> Read(int id)
        {
            try
            {
                return Ok(_repository.Read(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Controller exception on Read(int id)");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Get all Planets on the document
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<PlanetsModel>> ReadAll()
        {
            try
            {
                return Ok(_repository.ReadAll());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: controller exception on ReadAll()");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update Planet on the document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<bool> Update(int id, PlanetsModel entity)
        {
            try
            {
                return Ok(_repository.Update(id, entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Controller exception on Update(int id, T entity)");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Delete Planet on the document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                return Ok(_repository.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: Controller exception on  Delete(int id)");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }
    }
}
