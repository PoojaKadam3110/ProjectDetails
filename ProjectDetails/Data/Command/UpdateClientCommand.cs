using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Data.Query;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System.Runtime.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectDetailsAPI.Data.Command
{
    public class UpdateClientsCommand : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id{ get; set; }
        public Clients clients { get; set; }
    }

    public class UpdateClientsCommandHandlers : IRequestHandler<UpdateClientsCommand, QueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public UpdateClientsCommandHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)
        {
            _clientRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(UpdateClientsCommand request, CancellationToken cancellationToken)
        {
            var userLists = await _clientRepository.UpdateClients(request.id,request.clients);
            //var data = await _dbcontext.Clients.Where(x => x.isDeleted == false).UpdateClients(x => x.Id == request.id);


            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new QueryResponse()
            {
                Data = userLists?? default,
                IsSuccessful = userLists != null,
                Errors = userLists != null ? default : new() { $"You Can not insert new client please contact to admin!!!" }
            };
        }
    }

}
