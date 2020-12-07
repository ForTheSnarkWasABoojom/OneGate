using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OneGate.Backend.Gateway.Extensions;

namespace OneGate.Backend.Gateway.EventHubs
{
    public class AccountEventHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.GetAccountId().ToString());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.GetAccountId().ToString());
            await base.OnDisconnectedAsync(exception);
        }
    }
}