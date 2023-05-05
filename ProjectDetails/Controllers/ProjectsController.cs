using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.CustomeActionFilters;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Data.Command;
using ProjectDetailsAPI.Data.Query;
using ProjectDetailsAPI.GenericRepo;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class ProjectsController : ControllerBase
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsController(ProjectDetailsDbContext projectDetailsDbContext, IMapper mapper, IMediator mediator,IUnitOfWork unitOfWork)//,IMediator mediator , IRepo<T> repo
        {
            _dbcontext = projectDetailsDbContext;
            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("/api/Projects/List")]
        public ActionResult Get([FromQuery] int pageNumber = 1, int pageSize = 1000)
        {
            var clientsFromRepo = _unitOfWork.Projects.GetAll(pageNumber, pageSize).Where(x => x.isDeleted == false);

            return Ok(clientsFromRepo);
        }

      
        [HttpPost("/api/Projects/Create")]
        [ValidateModule]
        public async Task<Projects> AddProjects(Projects projects)
        {
            var response = await _mediator.Send(new AddProjectsCommand
            {
                projects = projects
            });

            if (response.IsSuccessful == false)
            {
                return null;
            }
            return projects;
        }
        
        [HttpGet("/api/Projects/By/Id")]
        public ActionResult GetById(int id)
        {
            var clientsFromRepo = _unitOfWork.Projects.GetById(id);
            if (clientsFromRepo == null || clientsFromRepo.isDeleted == true)
            {
                return NotFound("data may be deleted Or not inserted yet,please try again");
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

            if(response.IsSuccessful == false)
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
            if(response == null)
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


