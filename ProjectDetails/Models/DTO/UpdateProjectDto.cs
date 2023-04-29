﻿using System.ComponentModel.DataAnnotations;

namespace ProjectDetailsAPI.Models.DTO
{
    public class UpdateProjectDto
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public double projectCost { get; set; }
        public string projectManager { get; set; }
        public double ratePerHour { get; set; }
        public string projectUsers { get; set; }
        [MaxLength(1000)]
        public string description { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
