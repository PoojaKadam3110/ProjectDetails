using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Query
{
    public class GetClientByIdQuery : IRequest<QueryResponse>
    {
        private readonly IConfiguration _configuration;
        [DataMember]
        public int id { get; set; }
    }
    public class GetClientByIdQueryHandlers : IRequestHandler<GetClientByIdQuery, QueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ProjectDetailsDbContext _dbcontext;

        public GetClientByIdQueryHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)
        {
            _clientRepository = clientRepository;
            _dbcontext = context;
        }

        public async Task<QueryResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
                      
            var data = await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);
           
            return new QueryResponse()
            {
                Data = data ?? default,
                IsSuccessful = data != null,
                Errors = data != null ? default : new() { $"No Records Found Or may be delete that record of Id is {request.id} !!!" }

            };

        }
    }
}
