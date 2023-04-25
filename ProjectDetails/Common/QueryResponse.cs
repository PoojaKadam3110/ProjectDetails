using Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectDetailsAPI.Common
{
    public class QueryResponse : Response
    {
        public dynamic Data { get; set; }

        public QueryResponse() { }
        public QueryResponse(string Message, bool status = false)
        {
            IsSuccessful = status;
            Errors.Add(Message);
        }

        public QueryResponse(List<string> Messages, bool status = false)
        {
            IsSuccessful = status;
            Errors.AddRange(Messages);
        }
    }
}



