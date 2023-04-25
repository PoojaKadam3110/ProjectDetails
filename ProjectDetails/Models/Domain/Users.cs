using System.ComponentModel.DataAnnotations;

namespace ProjectDetailsAPI.Models.Domain
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public double PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        //Navigation Property
        public IEnumerable<Projects> IntProjects1 { get; set; }

    }
}
