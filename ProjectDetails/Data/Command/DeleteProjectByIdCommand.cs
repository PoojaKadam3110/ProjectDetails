using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Command
{   
    public class DeleteProjectByIdCommand : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id { get; set; }
    }

    public class DeleteProjectByIdCommandHandlers : IRequestHandler<DeleteProjectByIdCommand, QueryResponse>
    {
        private readonly IAddProjectsRepository _projectRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public DeleteProjectByIdCommandHandlers(IAddProjectsRepository clientRepository, ProjectDetailsDbContext context)
        {
            _projectRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
        {
            var dataDelete = await _projectRepository.DeleteProjectById(request.id);

            return new QueryResponse()
            {
                Data = dataDelete ?? default,
                IsSuccessful = dataDelete != null,
                Errors = dataDelete != null ? default : new() { "Not able to find the id: " +request.id + " May be deleted this id, please first insert data only after that we can able to delete data!!!" }
            };
        }
    }
}
