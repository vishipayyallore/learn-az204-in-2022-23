using Collage.Core.Entities;
using Collage.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Collage.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfessorsController : ControllerBase
    {

        private readonly ILogger<ProfessorsController> _logger;
        private readonly IProfessorsCosmosBll _professorsCosmosBll;

        public ProfessorsController(ILogger<ProfessorsController> logger, IProfessorsCosmosBll professorsCosmosBll)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _professorsCosmosBll = professorsCosmosBll ?? throw new ArgumentNullException(nameof(professorsCosmosBll));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Professor>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Professor>>> Get()
        {
            IEnumerable<Professor> professors;

            _logger.Log(LogLevel.Debug, "Request Received for ProfessorsController::Get");

            professors = await _professorsCosmosBll.GetAllProfessors();

            _logger.Log(LogLevel.Debug, "Returning the results from ProfessorsController::Get");

            return Ok(professors);
        }

        [HttpGet("{id}", Name = nameof(GetProfessorById))]
        [ProducesResponseType(typeof(Professor), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Professor>> GetProfessorById(Guid id)
        {
            Professor professor;
            _logger.Log(LogLevel.Debug, "Request Received for ProfessorsController::GetProfessorById");

            professor = await _professorsCosmosBll.GetProfessorById(id);

            if (professor == null)
            {
                return NotFound();
            }

            _logger.Log(LogLevel.Debug, "Returning the results from ProfessorsController::GetProfessorById");

            return Ok(professor);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Professor), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Professor>> AddProfessor([FromBody] Professor professor)
        {
            _logger.Log(LogLevel.Debug, "Request Received for ProfessorsController::AddProfessor");

            var createdProfessor = await _professorsCosmosBll.AddProfessor(professor);

            _logger.Log(LogLevel.Debug, "Returning the results from ProfessorsController::AddProfessor");

            return CreatedAtRoute(routeName: nameof(GetProfessorById),
                                  routeValues: new { id = createdProfessor.ProfessorId },
                                  value: createdProfessor);
        }

        // PUT: HTTP 200 / HTTP 204 should imply "resource updated successfully". 
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateProfessor([FromBody] Professor professor)
        {
            var _ = await _professorsCosmosBll.UpdateProfessor(professor);

            return NoContent();
        }

        // DELETE: HTTP 200 / HTTP 204 should imply "resource deleted successfully".
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteProfessor(Guid id)
        {
            var professorDeleted = await _professorsCosmosBll.DeleteProfessorById(id);

            if (!professorDeleted)
            {
                return StatusCode(500, $"Unable to delete Professor with id {id}");
            }

            return NoContent();
        }

    }
}