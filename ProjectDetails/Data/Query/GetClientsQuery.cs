using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Query
{
    public class GetClientsQuery : IRequest<QueryResponse>    {
        private readonly IConfiguration _configuration;

    }    public class GetClientsQueryHandlers : IRequestHandler<GetClientsQuery, QueryResponse>    {        private readonly IClientRepository _clientRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public GetClientsQueryHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)        {            _clientRepository = clientRepository;            _dbcontext = context;        }        public async Task<QueryResponse> Handle(GetClientsQuery request, CancellationToken cancellationToken)        {            var userLists = await _clientRepository.GetUsers();

            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new QueryResponse()            {
                Data = userLists.Any() ? userLists : default,
                IsSuccessful = userLists.Any(),
                Errors = userLists.Any() ? default : new() { $"No Matching Records Found !!!" }
            };        }

     }
}
