using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Services.Interfaces
{
    public interface IClientsService
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client?> GetClientById(int id);
        Task<OperationResult> CreateClient(CreateClientDto client);
        Task<OperationResult> EditClient(EditClientDto client);
        Task<OperationResult> DeleteClient(int id);

    }
}
