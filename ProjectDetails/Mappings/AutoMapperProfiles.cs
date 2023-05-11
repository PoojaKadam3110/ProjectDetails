using AutoMapper;
using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Projects, Projects>().ReverseMap();
        }
    }
}
