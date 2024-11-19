using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApp.DTO;
using TestWebApp.Services;

namespace TestWebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClientController(IClientService clientService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<List<ClientDTO>>> GetAll()
    {
        var clients = await clientService.GetAll();
        return Ok(clients);
    }
    
    [HttpPost]
    public async Task<ActionResult<ClientDTO>> Add(ClientDTO clientDto)
    {
        try
        {
            var client = await clientService.Add(clientDto);
            return Ok(client);
        }
        catch (DbUpdateException exception)
        {
            return BadRequest(exception.Message);
        }
       
    }
    
    [HttpGet("{clientId}")]
    public async Task<ActionResult<ClientDTO>> Get(long clientId)
    {
        if (clientId <0)
        {
            return BadRequest("ClientId is invalid");
        }
        
        var client = await clientService.Get(clientId);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }
    
    [HttpPut("{clientId}")]
    public async Task<ActionResult<ClientDTO>> Update(long clientId, ClientDTO clientDto)
    {
        if (clientId <0)
        {
            return BadRequest("clientId is invalid");
        }

        if (clientId != clientDto.ClientId)
        {
            return BadRequest("ClientId in param and clientId in model is different ");
        }
        
        var client = await clientService.Update(clientDto);
        return Ok(client);
    }

    [HttpPost("checkduplicates")]
    public async Task<ActionResult<List<ClientDTO>>> AddClients(List<ClientDTO> clientsDto)
    {
        var clients = await clientService.AddClients(clientsDto);
        return Ok(clients);
    }
    
    [HttpDelete("{clientId}")]
    public async Task<ActionResult> Delete(long clientId)
    {
        if (clientId <0)
        {
            return BadRequest("ClientId is invalid");
        }
        
        await clientService.Delete(clientId);
        return Ok();
    }
    
}