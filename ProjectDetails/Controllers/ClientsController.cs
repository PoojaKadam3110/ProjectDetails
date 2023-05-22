using Microsoft.AspNetCore.Mvc;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/api/Clients/List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Get()
        {
            var clientsFromRepo = _unitOfWork.Clients.GetAll().Where(x => x.isDeleted == false);

            return Ok(clientsFromRepo);
        }

        [HttpGet("/api/Clients/UsersList")]
        public ActionResult GetAllUsers()
        {
            var clientsFromRepo = _unitOfWork.Users.GetAll().Where(x => x.isDeleted == false);

            return Ok(clientsFromRepo);
        }
        [HttpGet("/api/Clients/By/Id")]
        public ActionResult GetById(int id)
        {
            var clientsFromRepo = _unitOfWork.Clients.GetById(id);
            if (clientsFromRepo == null || clientsFromRepo.isDeleted == true)
            {
                return NotFound("Id " + id + " Not found may be deleted Or not inserted yet,please try again");
            }

            return Ok(clientsFromRepo);
        }

        [HttpPost("/api/clients/Add")]
        public ActionResult AddClient(Clients clients)
        {
            _unitOfWork.Clients.Add(clients);          
            return CreatedAtAction("GetById", new { id = clients.Id }, clients);
        }

        [HttpPut("/api/clients/Update")]
        public ActionResult Update(Clients clients)
        {

            clients.UpdatedDate = DateTime.Now;
            _unitOfWork.Clients.Update(clients);
            return Ok("Client Updated successfully Of Id: " + clients.Id);
        }

        [HttpDelete("/api/clients/Delete")]
        public ActionResult Delete(Clients clients)
        {
            clients.isActive = false;
            _unitOfWork.Clients.SoftDelete(clients);
            return Ok("Client Deleted successfully Of Id: " + clients.Id);
        }
    }
}
