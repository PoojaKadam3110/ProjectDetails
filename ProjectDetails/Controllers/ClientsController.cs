using Microsoft.AspNetCore.Http;
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
        public ActionResult Get()
        {
            var clientsFromRepo = _unitOfWork.Clients.GetAll();

            return Ok(clientsFromRepo);
        }
        [HttpGet("/api/Clients/By/Id")]
        public ActionResult GetById(int id)
        {
            var clientsFromRepo = _unitOfWork.Clients.GetById(id);

            return Ok(clientsFromRepo);
        }

        [HttpPost("/api/clients/Add")]
        public ActionResult AddClient(Clients clients)
        {
            _unitOfWork.Clients.Add(clients);
            return Ok("User Added successfully");    
        }

        [HttpPut("/api/clients/Update")]
        public ActionResult Update(Clients clients)
        {
            _unitOfWork.Clients.Update(clients);
            return Ok("User Updated successfully");
        }

        [HttpDelete("/api/clients/Delete")]
        public ActionResult Delete(Clients clients)
        {
            _unitOfWork.Clients.SoftDelete(clients);
            return Ok("User Deleted successfully");
        }
    }
}
