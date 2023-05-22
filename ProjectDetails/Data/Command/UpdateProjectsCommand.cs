using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Models.DTO;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Command
{
   
      public class UpdateProjectsCommand : IRequest<CommandResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id { get; set; }
        public UpdateProjectsDto projects { get; set; }
    }

    public class UpdateProjectsCommandHandlers : IRequestHandler<UpdateProjectsCommand, CommandResponse>
    {
        private readonly IProjectsRepository _projectRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public UpdateProjectsCommandHandlers(IProjectsRepository projectRepository, ProjectDetailsDbContext context)
        {
            _projectRepository = projectRepository;
            _dbcontext = context;
        }

        public async Task<CommandResponse> Handle(UpdateProjectsCommand request, CancellationToken cancellationToken)
        {
            var data = await _projectRepository.UpdateProject(request.id, request.projects);

            return new CommandResponse()
            {
                Data = data ?? default,
                IsSuccessful = data != null,
                Errors = data != null ? default : new() { $"You Can not able to Update project may be this project id is not available!!!" }
            };
        }
    }

}
