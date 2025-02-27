using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class MessageHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveConnectInfo", "User connected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("ReceiveDisconnectInfo", "User disconnected");
        }

        public async Task SendMessage(string message, double data)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message + "'s offer is ", data);
        }

        public async Task EndBid(string user, double data)
        {
            await Clients.All.SendAsync("EndBid", user + " won the bid with ", data);
        }
    }
}
