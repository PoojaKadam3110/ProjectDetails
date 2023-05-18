using System.ComponentModel.DataAnnotations;

namespace ProjectDetailsAPI.Models.DTO
{
    public class UpdateProjectsDto
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public double projectCost { get; set; }
        public string projectManager { get; set; }
        public double ratePerHour { get; set; }
        public string projectUsers { get; set; }
        [MaxLength(1000)]
        public string description { get; set; }
    }
}
