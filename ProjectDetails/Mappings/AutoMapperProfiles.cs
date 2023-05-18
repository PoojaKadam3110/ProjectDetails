using AutoMapper;
using ProjectDetailsAPI.Models.Domain;
using System.Drawing.Drawing2D;
using System.Drawing;
using ProjectDetailsAPI.Models.DTO;

namespace ProjectDetailsAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Projects, ProjectsDto>().ReverseMap();
        }
    }
}
