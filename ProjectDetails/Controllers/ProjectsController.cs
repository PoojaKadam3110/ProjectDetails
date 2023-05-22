using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectDetailsAPI.CustomeActionFilters;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Data.Command;
using ProjectDetailsAPI.Models.DTO;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using System.Data;

namespace ProjectDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private IProjectsRepository dataStore;

        public ProjectsController(ProjectDetailsDbContext projectDetailsDbContext, IMediator mediator, IUnitOfWork unitOfWork, IProjectsRepository addProjectsRepository)//,IMediator mediator , IRepo<T> repo
        {
            _dbcontext = projectDetailsDbContext;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            dataStore = addProjectsRepository;

        }

        [HttpGet("/api/Projects/List")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, int pageSize = 1000) //modified thid method for filtering middle two para for sorting and last two for paggination
        {
            try
            {
                var projectsDomainModel = await dataStore.GetAll(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

                var result = projectsDomainModel.Where(x => x.isDeleted == false);
                if (result != null && result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("This Project Name is not present in the database, please enter other name!!!!");
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("You are not able to see the project list");
            }

        }

        [HttpPost("/api/Projects/AddProjects")]
        [ValidateModule]
        public async Task<ActionResult<ProjectsDto>> AddProjects(ProjectsDto projectsFromClt)
        {
            try
            {
                var response = await _mediator.Send(new AddProjectsCommand
                {
                    projects = projectsFromClt
                });

                if (response.IsSuccessful == false)
                {
                    return null;
                }
                return CreatedAtAction("GetById", new { Id = response.Data.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpGet("ProjectsCount")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRecordCountProjects()
        {
            // Retrieve the count of records from the database
            int count = dataStore.GetRecordCount();
            // Return the count as a response to the client browser
            return Ok("Number of projects present in the database is: " + count);
        }

        [HttpGet("DeletedProjectsCount")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDeletedRecordCountProjects()
        {
            // Retrieve the count of records from the database
            int count = dataStore.GetDeletedRecordCount();
            // Return the count as a response to the client browser
            return Ok("Number of projects Deleted: " + count);
        }

        [HttpGet("/api/Projects/Clients/List")]
        public ActionResult Get([FromQuery] int pageNumber = 1, int pageSize = 1000)
        {
            var clientsFromRepo = _unitOfWork.Clients.GetAll(pageNumber, pageSize).Where(x => x.isDeleted == false);

            return Ok(clientsFromRepo);
        }     

        [HttpGet("/api/Projects/By/Id")]
        public ActionResult GetById(int id)
        {
            var clientsFromRepo = _unitOfWork.Projects.GetById(id);
            if (clientsFromRepo == null || clientsFromRepo.isDeleted == true)
            {
                return NotFound("Id " + id + " Not found may be deleted Or not inserted yet,please try again");
            }

            return Ok(clientsFromRepo);
        }


        [HttpPut("/api/Projects/Update/By/Id")]
        [ValidateModule]
        public async Task<UpdateProjectsDto> UpdateProjects(int id, UpdateProjectsDto projects)
        {
            var response = await _mediator.Send(new UpdateProjectsCommand
            {
                id = id,
                projects = projects
            });

            if (response.IsSuccessful == false)
            {
                return null;
            }

            return projects;
        }

        [HttpDelete("/api/Projects/Delete/By/Id")]
        [ValidateModule]
        public async Task<IActionResult> SoftDeleteProject(int id)
        {            
            var response = await _mediator.Send(new DeleteProjectByIdCommand
            {
                id = id
            });
            if (response.IsSuccessful == false)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
