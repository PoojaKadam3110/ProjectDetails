using System.ComponentModel.DataAnnotations;

namespace ProjectDetailsAPI.Models.Domain
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int ProjectHeadId { get; set; }
        public int ProjectManagerId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        //public int ClientId { get; set; }
        public int UsersId { get; set; }
        public int ClientsId { get; set; }

        //Navigation Property

        //public IntUsers1 Intusers1 { get; set; }
        //public Clients Intclients1  { get; set; }

    }
}
