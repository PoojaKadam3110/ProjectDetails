using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Data.Query
{
    public class GetClientsQuery : IRequest<QueryResponse>    {
        private readonly IConfiguration _configuration;    }    public class GetClientsQueryHandlers : IRequestHandler<GetClientsQuery, QueryResponse>    {        private readonly IClientRepository _clientRepository;        public GetClientsQueryHandlers(IClientRepository clientRepository)        {            _clientRepository = clientRepository;        }        public async Task<QueryResponse> Handle(GetClientsQuery request, CancellationToken cancellationToken)        {            var userLists = await _clientRepository.GetUsers();           // var userListsById = await _clientRepository.GetUsersById(int id);            return new QueryResponse()            {                Data = userLists.Any() ? userLists : default,                IsSuccessful = userLists.Any(),                Errors = userLists.Any() ? default : new() { $"No Matching Records Found !!!" }            };        }    }
}
