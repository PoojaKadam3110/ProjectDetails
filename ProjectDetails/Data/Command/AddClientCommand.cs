using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Data.Query;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System.Runtime.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectDetailsAPI.Data.Command
{
    public class AddClientCommand : IRequest<CommandResponse>
    {
        private readonly IConfiguration _configuration;
        public Clients clients{ get; set; }
    }

    public class AddClientCommandHandlers : IRequestHandler<AddClientCommand, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public AddClientCommandHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)
        {
            _clientRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<CommandResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var userLists = await _clientRepository.AddClients(request.clients);

            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new CommandResponse()
            {
                Data = userLists?? default,
                IsSuccessful = userLists != null,
                Errors = userLists != null ? default : new() { $"You Can not insert new client!!!" }
            };
        }
    }

}
