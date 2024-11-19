using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Repositories;
using TestWebApp.DTO;
using TestWebApp.Extensions;

namespace TestWebApp.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<ClientDTO> Add(ClientDTO clientModel)
    {
        return await clientRepository.Add(clientModel.ToModel());
    }

    public async Task<ClientDTO?> Get(long id)
    {
        var client = await clientRepository.GetById(id);
        if (client == null)
        {
            return null;
        }
        else return client;
    }

    public async Task<List<ClientDTO>?> GetAll()
    {
        return await clientRepository.GetAll();
    }

    public async Task<ClientDTO> Update(ClientDTO client)
    {
       var clientUpdated =  await clientRepository.Update(client.ToModel());
       return clientUpdated;
    }

    public async Task<List<ClientDTO>?> AddClients(List<ClientDTO> clientsDto)
    {
        var clientsModel = clientsDto.Select(x => x.ToModel()).ToList();
        var clients = await clientRepository.GetAll(clientsModel);

        return clients;
    }

    public async Task Delete(long id)
    {
        await clientRepository.Delete(id);
    }
}