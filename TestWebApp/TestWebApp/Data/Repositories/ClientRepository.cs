using Microsoft.EntityFrameworkCore;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Models;
using TestWebApp.DTO;
using TestWebApp.Extensions;

namespace TestWebApp.Data.Repositories;

public class ClientRepository(AppDBContext dbContext) : IClientRepository
{
    public async Task<List<ClientDTO>?> GetAll()
    {
        var clients = await dbContext.Clients.ToListAsync();
        return clients.Count == 0 ? null : clients.Select(x=>x.ToDto()).ToList();
    }

    public async Task<List<ClientDTO>?> GetAll(List<Client> clientsModel)
    {
        
        var idsClients = clientsModel.Select(x => x.ClientId).ToList();
        
        var oldClientIds = await dbContext.Clients.AsNoTracking().Where(x => idsClients.Contains(x.ClientId)).Select(x=>x.ClientId).ToListAsync();

        var newClient = clientsModel.Where(x => !oldClientIds.Contains(x.ClientId)).ToList();

        if (newClient.Count > 0)
        {
            dbContext.ChangeTracker.Clear();
            await dbContext.Clients.AddRangeAsync(newClient);
            await dbContext.SaveChangesAsync();
        }

        var result = clientsModel.Where(x => oldClientIds.Contains(x.ClientId)).Select(x => x.ToDto()).ToList();
        
        return result;


    }

    public async Task<ClientDTO> Add(Client clientModel)
    {
            await dbContext.Clients.AddAsync(clientModel);
            await dbContext.SaveChangesAsync();
            return clientModel.ToDto();
        
    }

    public async Task<ClientDTO?> GetById(long id)
    {
        var client = await dbContext.Clients.Where(x => x.ClientId == id).FirstOrDefaultAsync();
        return client?.ToDto();
    }

    public async Task<ClientDTO> Update(Client clientModel)
    {
        dbContext.Clients.Update(clientModel);
        await dbContext.SaveChangesAsync();

        return clientModel.ToDto();
    }

    public async Task Delete(long id)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(x=>x.ClientId==id);
        if (client != null)
        {
            dbContext.Clients.Remove(client);
            await dbContext.SaveChangesAsync();
        }
    }
}