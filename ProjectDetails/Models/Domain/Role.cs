﻿namespace ProjectDetailsAPI.Models.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
