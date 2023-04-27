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
    public class AddClientCommand : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        public Clients clients{ get; set; }
    }

    public class AddClientCommandHandlers : IRequestHandler<AddClientCommand, QueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public AddClientCommandHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)
        {
            _clientRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var userLists = await _clientRepository.AddClients(request.clients);

            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new QueryResponse()
            {
                Data = userLists?? default,
                IsSuccessful = userLists != null,
                Errors = userLists != null ? default : new() { $"You Can not insert new client!!!" }
            };
        }
        //private readonly ProjectDetailsDbContext _dbcontext;
        //private readonly IClientRepository _clientRepository;
        //public AddClientCommand(ProjectDetailsDbContext dbContext,IClientRepository client) 
        //{ 
        //    _dbcontext = dbContext;
        //    _clientRepository = client;
        //}

        //public async Task<Clients> Insert(Clients clients)
        //{
        //    await _dbcontext.Clients.AddAsync(clients);
        //    await _dbcontext.SaveChangesAsync();

        //    return clients;
        //}
        //public async Task<QueryResponse> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        //{
        //    var userLists = await _clientRepository.AddClients();

        //    //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

        //    return new QueryResponse()
        //    {
        //        Data = userLists.Any() ? userLists : default,
        //        IsSuccessful = userLists.Any(),
        //        Errors = userLists.Any() ? default : new() { $"No Matching Records Found !!!" }
        //    };
        //}

    }

}
