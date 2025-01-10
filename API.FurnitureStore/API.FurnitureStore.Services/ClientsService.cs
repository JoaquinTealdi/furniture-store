using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos.Client;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.Services
{
    public class ClientsService : IClientsService
    {
        private readonly FugnitureStoreDbContext _context;

        public ClientsService(FugnitureStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<OperationResult> CreateClient(CreateClientDto client)
        {
            try
            {
                var response = new OperationResult();

                #region Create obj
                var newClient = new Client
                {
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    BirthDate = client.BirthDate,
                    PhoneNumber = client.PhoneNumber,
                    Address = client.Address
                };

                #endregion



                _context.Clients.Add(newClient);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Client successfully created.";
                    response.ResourceId = newClient.Id;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Client could not be created.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }
        public async Task<OperationResult> EditClient(EditClientDto client)
        {
            try
            {
                var response = new OperationResult();
                var clientExists = _context.Clients.Find(client.Id);

                if (clientExists == null)
                {
                    response.Success = false;
                    response.Message = "Client Not Found.";

                    return response;
                }

                clientExists.FirstName = client.FirstName ?? clientExists.FirstName;
                clientExists.LastName = client.LastName ?? clientExists.LastName;
                clientExists.BirthDate = client.BirthDate ?? clientExists.BirthDate;
                clientExists.PhoneNumber = client.PhoneNumber ?? clientExists.PhoneNumber;
                clientExists.Address = client.Address ?? clientExists.Address;


                _context.Clients.Update(clientExists);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Client successfully edited.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Client could not be edited.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }

        public async Task<OperationResult> DeleteClient(int id)
        {
            try
            {
                var response = new OperationResult();
                var client = _context.Clients.Find(id);

                if (client == null)
                {
                    response.Success = false;
                    response.Message = "Client Not Found.";

                    return response;
                }

                _context.Clients.Remove(client);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Client successfully deleted.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Client could not be deleted.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }
    }
}
