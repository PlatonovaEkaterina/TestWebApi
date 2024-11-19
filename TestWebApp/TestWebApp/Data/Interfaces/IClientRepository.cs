using TestWebApp.Data.Models;
using TestWebApp.DTO;

namespace TestWebApp.Data.Interfaces;

public interface IClientRepository
{
    Task<List<ClientDTO>?> GetAll();
    Task<List<ClientDTO>?> GetAll(List<Client> idsClients);
    Task<ClientDTO> Add(Client clientModel);
    Task<ClientDTO?> GetById(long id);
    Task<ClientDTO> Update(Client clientModel);
    Task Delete(long id);
}