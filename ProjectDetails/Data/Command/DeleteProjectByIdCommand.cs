using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Command
{
    public class DeleteProjectByIdCommand : IRequest<CommandResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id { get; set; }
    }

    public class DeleteProjectByIdCommandHandlers : IRequestHandler<DeleteProjectByIdCommand, CommandResponse>
    {
        private readonly IProjectsRepository _projectRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public DeleteProjectByIdCommandHandlers(IProjectsRepository clientRepository, ProjectDetailsDbContext context)
        {
            _projectRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<CommandResponse> Handle(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
        {
            var dataDelete = await _projectRepository.DeleteProjectById(request.id);

            return new CommandResponse()
            {
                Data = dataDelete ?? default,
                IsSuccessful = dataDelete != null,
                Errors = dataDelete != null ? default : new() { "Not able to found this id, please first insert data only after that we can able to delete data!!!" }
            };
        }
    }
}
