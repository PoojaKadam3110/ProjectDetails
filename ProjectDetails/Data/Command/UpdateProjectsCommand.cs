using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Command
{
   
      public class UpdateProjectsCommand : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id { get; set; }
        public Projects projects { get; set; }
    }

    public class UpdateProjectsCommandHandlers : IRequestHandler<UpdateProjectsCommand, QueryResponse>
    {
        private readonly IAddProjectsRepository _projectRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public UpdateProjectsCommandHandlers(IAddProjectsRepository projectRepository, ProjectDetailsDbContext context)
        {
            _projectRepository = projectRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(UpdateProjectsCommand request, CancellationToken cancellationToken)
        {
            var data = await _projectRepository.UpdateProject(request.id, request.projects);

            return new QueryResponse()
            {
                Data = data ?? default,
                IsSuccessful = data != null,
                Errors = data != null ? default : new() { $"You Can not Update project may be not available!!!" }
            };
        }
    }

}
