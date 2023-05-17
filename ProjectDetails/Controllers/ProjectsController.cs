using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using ProjectDetailsAPI.CustomeActionFilters;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Data.Command;
using ProjectDetailsAPI.Data.Query;
using ProjectDetailsAPI.GenericRepo;
using ProjectDetailsAPI.Models.Domain;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private IAddProjectsRepository dataStore;

        public ProjectsController(ProjectDetailsDbContext projectDetailsDbContext, IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork, IAddProjectsRepository addProjectsRepository)//,IMediator mediator , IRepo<T> repo
        {
            _dbcontext = projectDetailsDbContext;
            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            dataStore = addProjectsRepository;

        }

        [HttpGet("/api/Projects/List")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, int pageSize = 1000) //modified thid method for filtering middle two para for sorting and last two for paggination
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


        //[HttpGet("/api/Projects/Users/List")]
        //public ActionResult GetAllUsers()
        //{
        //    var clientsFromRepo = _unitOfWork.Users.GetAll().Where(x => x.isDeleted == false);

        //    return Ok(clientsFromRepo);
        //}


        [HttpGet("/api/Projects/Clients/List")]
        //[Authorize(Roles = "Admin")]
        public ActionResult Get([FromQuery] int pageNumber = 1, int pageSize = 1000)
        {
            var clientsFromRepo = _unitOfWork.Clients.GetAll(pageNumber, pageSize).Where(x => x.isDeleted == false);

            return Ok(clientsFromRepo);
        }

        [HttpPost("/api/Projects/AddProjects")]
        [ValidateModule]
        public async Task<ActionResult<Projects>> AddProjects(Projects projects)
        {
            var response = await _mediator.Send(new AddProjectsCommand
            {
                projects = projects
            });

            if (response.IsSuccessful == false)
            {
                return null;
            }
            //return projects;
            //return CreatedAtAction(nameof(GetById), new { id = projects.Id }, projects);
            return CreatedAtAction("GetById", new { id = projects.Id }, projects);
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
        public async Task<Projects> UpdateProjects(int id, Projects projects)
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
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);            //Ok("project deleted successfully!!!");
        }
    }
}




































//[HttpGet]
//public async Task<IEnumerable<Clients>> GetClients() => await _mediator.Send(new GetClients.Query());


//[HttpGet("/api/Projects/Clients/List")]
//public async Task<ActionResult<List<Clients>>> GetAll()
//{            
//    var clientsDetails =await _mediator.Send(new GetClientsQuery());
//    // return await _dbcontext.Clients.Where(x => x.isDeleted == false).ToListAsync();
//    return Ok(clientsDetails);
//}

//[HttpGet("/api/Projects/List")]
//public ActionResult Get()
//{
//    var clientsFromRepo = _unitOfWork.Projects.GetAll().Where(x => x.isDeleted == false);

//    return Ok(clientsFromRepo);
//}

//[HttpPost("/api/Projects/Client/Create")]
//[ValidateModule]

//public async Task<Clients> AddClients(Clients clients)
//{
//    var response = await _mediator.Send(new AddClientCommand
//    {
//        clients = clients
//    });
//    return clients;
//}


//[HttpGet("/api/Projects/Client/ById")]   //("/api/Projects/Client/ById")
//[ValidateModule]
////[Route("{id:int}")]
//public async Task<IActionResult> GetClientsDetailsById(int id)
//{
//    var response = await _mediator.Send(new GetClientByIdQuery
//    {
//        id = id
//    });
//    return Ok(response);
//}
//[HttpPut("/api/Projects/Clients")]
//[ValidateModule]
//public async Task<Clients> UpdateClients(int id,Clients clients)
//{
//    var response = await _mediator.Send(new UpdateClientsCommand
//    {
//        id = id,
//        clients = clients
//    });

//    return clients;        
//}
//[HttpDelete("/api/Projects/Clients/Delete")]
//[ValidateModule]
//public async Task<IActionResult> SoftDeleteClient(int id)
//{
//    var response = await _mediator.Send(new DeleteClientByIdQueryCommand
//    {
//        id = id
//    });
//    return Ok(response);
//}


