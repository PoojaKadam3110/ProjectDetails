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
        //private readonly IProjectRepository _projectRepository;
        //private readonly IRepo<T> _repo;          //For generic
        public ProjectsController(ProjectDetailsDbContext projectDetailsDbContext, IMapper mapper, IMediator mediator)//,IMediator mediator , IRepo<T> repo
        {
            _dbcontext = projectDetailsDbContext;
            _mapper = mapper;
            _mediator = mediator;
            //_projectRepository = projectRepository;
            //_repo = repo;               //For generic
                                             // this._mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Clients>> GetClients() => await _mediator.Send(new GetClients.Query());
 

        [HttpGet("/api/Projects/Clients/List")]
        //public async Task<ActionResult<List<Clients>>> GetClientsDetails()
        public async Task<ActionResult<List<Clients>>> GetAll()
        {            
            var clientsDetails =await _mediator.Send(new GetClientsQuery());
            // return await _dbcontext.Clients.Where(x => x.isDeleted == false).ToListAsync();
            return Ok(clientsDetails);
        }
        //public List<T> GetAll()
        //{
        //    //return await _dbcontext.Clients.Where(x => x.isDeleted == false).ToListAsync();
        //    return _repo.GetAll();
        //}

        [HttpPost("/api/Projects/Client/Create")]
        [ValidateModule]

        public async Task<Clients> AddClients(Clients clients)
        {
            await _dbcontext.Clients.AddAsync(clients);
            await _dbcontext.SaveChangesAsync();

            return clients;
        }

        [HttpGet("/api/Projects/Client/ById")]   //("/api/Projects/Client/ById")
        [ValidateModule]
        //[Route("{id:int}")]
        public async Task<IActionResult> GetClientsDetailsById(int id)
        {
            var response = await _mediator.Send(new GetClientByIdQuery
            {
                id = id
            });
            return Ok(response);
        }
        //}
        //[ValidateModule]
        //[Route("{id:int}")]

        //public async Task<Clients> AddClients(Clients clients)
        //{
        //    var addClient = await _mediator.Send(new AddClientCommand
        //    {
        //        _dbcontext.Clients = clients
        //    });
        //    //await _dbcontext.Clients.AddAsync(clients);
        //    //await _dbcontext.SaveChangesAsync();

        //    return addClient;
        //}

        //public async Task<Clients> GetClientsDetailsById(int id)
        //{           
        //    var clientsDetailsById = await _mediator.Send(new GetClientsQuery())
        //    {
        //        id = id;
        //    };

        //    return Ok(clientsDetailsById);

        //    //return await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
        //    // return await _dbcontext.Clients.Where(x => x.isDeleted == false).ToListAsync();
        //    //var clientsDetailsforId = await _mediator.(new GetClientsQuery());
        //    //return Ok(clientsDetailsforId);
        //}


        [HttpPut("/api/Projects/Clients/By/Id")]
        [ValidateModule]
        //[HttpPut]
        //[Route("{id:int}")]
        public async Task<Clients> UpdateClients(int id,Clients clients)
        {
            var existingClient = await _dbcontext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (existingClient == null) 
            {
                return null;
            }
            existingClient.ClientName = clients.ClientName;
            existingClient.UpdatedDate = clients.UpdatedDate;
            existingClient.isDeleted = clients.isDeleted;
            existingClient.CreatedBy = clients.CreatedBy;
            existingClient.UpdatedBy = clients.UpdatedBy;

            await _dbcontext.SaveChangesAsync();

            return existingClient;
        }

        [HttpDelete("/api/Projects/Clients/Delete/By/Id")]
        [ValidateModule]
        //[HttpDelete]        
        //[Route("{id:int}")]

        public async Task<Clients?> SoftDeleteClient(int id)
        {
            var existingClient = await _dbcontext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if(existingClient == null)
            {
                return null;
            }

            existingClient.isDeleted = true;

            await _dbcontext.SaveChangesAsync();    

            return existingClient;
            //return response()->json("User deleted successfully!!!");
            //return Ok("User deleted successfully!!!");
        }
    }
}
