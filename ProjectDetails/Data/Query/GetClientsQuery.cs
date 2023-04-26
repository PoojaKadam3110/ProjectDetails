﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Common;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System.Runtime.Serialization;

namespace ProjectDetailsAPI.Data.Query
{
    public class GetClientsQuery : IRequest<QueryResponse>
        private readonly IConfiguration _configuration;

    }
        private readonly ProjectDetailsDbContext _dbcontext;

        public GetClientsQueryHandlers(IClientRepository clientRepository, ProjectDetailsDbContext context)

            //var data =  await _dbcontext.Clients.Where(x => x.isDeleted == false).FirstOrDefaultAsync(x => x.Id == request.id);

            return new QueryResponse()
                Data = userLists.Any() ? userLists : default,
                IsSuccessful = userLists.Any(),
                Errors = userLists.Any() ? default : new() { $"No Matching Records Found !!!" }
            };

     }
}