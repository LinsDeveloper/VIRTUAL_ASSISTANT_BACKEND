using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.Interfaces
{
    public interface IHttpClientService
    {
        Task<TResponse> GetAsync<TResponse>(string clientName, string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string clientName, string endpoint, TRequest body);
    }
}
