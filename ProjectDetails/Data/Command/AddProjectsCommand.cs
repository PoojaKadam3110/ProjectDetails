using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Data.Command
{
    public class AddProjectsCommand : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        public Projects projects { get; set; }
    }

    public class AddProjectsCommandHandlers : IRequestHandler<AddProjectsCommand, QueryResponse>
    {
        private readonly IAddProjectsRepository _projectRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public AddProjectsCommandHandlers(IAddProjectsRepository projectRepository, ProjectDetailsDbContext context)
        {
            _projectRepository = projectRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(AddProjectsCommand request, CancellationToken cancellationToken)
        {
            var addProject = await _projectRepository.AddProjects(request.projects);

            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new QueryResponse()
            {
                Data = addProject ?? default,
                IsSuccessful = addProject != null,
                Errors = addProject != null ? default : new() { $"You Can not adding new Project" }
            };
        }
    }
}
