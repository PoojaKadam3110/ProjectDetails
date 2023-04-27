using MediatR;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Command
{
    public class DeleteClientByIdQueryCommand : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id { get; set; }
    }

    public class DeleteClientByIdQueryCommandHandlers : IRequestHandler<DeleteClientByIdQueryCommand, QueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public DeleteClientByIdQueryCommandHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)
        {
            _clientRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(DeleteClientByIdQueryCommand request, CancellationToken cancellationToken)
        {
            var userLists = await _clientRepository.DeleteClientById(request.id);

            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new QueryResponse()
            {
                Data = userLists ?? default,
                IsSuccessful = userLists != null,
                Errors = userLists != null ? default : new() { $"You Can not delete client!!!" }
            };
        }
    }
}
