using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Models.DTO;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Data.Command
{
    public class AddProjectsCommand : IRequest<CommandResponse>
    {
        private readonly IConfiguration _configuration;
        public ProjectsDto projects { get; set; }
    }

    public class AddProjectsCommandHandlers : IRequestHandler<AddProjectsCommand, CommandResponse>
    {
        private readonly IProjectsRepository _projectRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public AddProjectsCommandHandlers(IProjectsRepository projectRepository, ProjectDetailsDbContext context)
        {
            _projectRepository = projectRepository;
            _dbcontext = context;
        }

        public async Task<CommandResponse> Handle(AddProjectsCommand request, CancellationToken cancellationToken)
        {
            var addProject = await _projectRepository.AddProjects(request.projects);
           
            return new CommandResponse()
            {
                Data = addProject ?? default,
                IsSuccessful = addProject != null,
                Errors = addProject != null ? default : new() { $"You Can not adding new Project!!!" }
            };
        }
    }
}
