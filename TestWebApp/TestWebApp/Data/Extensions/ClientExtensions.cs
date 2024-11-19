using TestWebApp.Data.Models;
using TestWebApp.DTO;

namespace TestWebApp.Extensions;

public static class ClientExtensions
{
    public static ClientDTO ToDto(this Client client)
    {
        return new ClientDTO()
        {
            ClientId = client.ClientId,
            Username = client.Username,
            SystemId = client.SystemId
        };
    }
    
    public static Client ToModel(this ClientDTO clientDto)
    {
        return new Client()
        {
            ClientId = clientDto.ClientId,
            Username = clientDto.Username,
            SystemId = clientDto.SystemId
        };
    }
}