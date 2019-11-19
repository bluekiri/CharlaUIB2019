using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWarsAPI.Domain.Models.SpeciesAggregate;
using System;
using System.Collections.Generic;
using System.Net;

namespace StarWarsAPI.Controllers
{
    /// <summary>
    /// SpeciesController
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesRepository _repository;
        private readonly ILogger<SpeciesController> _logger;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public SpeciesController(ISpeciesRepository service, ILogger<SpeciesController> logger)
        {
            _repository = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Create Specie on the document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> Create(SpeciesModel input)
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
        /// Get Specie on the document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<SpeciesModel> Read(int id)
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
        /// Get all Species on the document
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<SpeciesModel>> ReadAll()
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
        /// Update Specie on the document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<bool> Update(int id, SpeciesModel entity)
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
        /// Delete Specie on the document
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
