using TestWebApp.DTO;

namespace TestWebApp.Services;

public interface IClientService
{
    Task<ClientDTO> Add(ClientDTO client);
    Task<ClientDTO?> Get(long id);
    Task<List<ClientDTO>?> GetAll();
    Task<List<ClientDTO>?> AddClients(List<ClientDTO> clientsDto);
    Task<ClientDTO> Update(ClientDTO client);
    Task Delete(long id);
}