using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Server.Models.Utils;

namespace Server.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{

    private readonly HubCallerContext _Context;

    public ChatHub(HubCallerContext context)
    {
        _Context = context;
    }


    public async Task SendMessageToEmployee(string message)
    {
        var userId = _Context.User!.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Broadcast the message to all employees (in this case, the company)
        await Clients.Group("Employee").ReceiveMessage(userId!, message);
    }

    public async Task SendMessageToClient(string clientId, string message)
    {
        // Broadcast the message to the specific client
        await Clients.User(clientId).ReceiveMessage("Employee", message);
    }

    // Override OnConnectedAsync to handle user joining
    public override async Task OnConnectedAsync()
    {
        var userId = _Context.User!.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isEmployee = _Context.User.HasClaim(ClaimTypes.Role, Common.Employee_Role);
        var groupName = isEmployee ? "Employee" : userId;

        await Groups.AddToGroupAsync(_Context.ConnectionId, groupName!);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(_Context.ConnectionId, "Employee");
        await Groups.RemoveFromGroupAsync(_Context.ConnectionId, _Context.User!.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        await base.OnDisconnectedAsync(exception);
    }
}