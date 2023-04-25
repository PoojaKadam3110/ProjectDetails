using System.ComponentModel.DataAnnotations;

namespace ProjectDetailsAPI.Models.DTO
{
    public class AddClientsDto
    {
        [Required]
        public string ClientName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
