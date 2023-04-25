using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.Services
{
    public interface IClientRepository
    {
        Task<List<ClientResponse>> GetUsers();
       // Task<List<ClientResponse>> GetUsersById(int id);
    }

    public class ClientResponse
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
