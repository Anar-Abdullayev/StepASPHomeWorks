using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class MessageHub (IFileService fileService) : Hub
    {
        public static Dictionary<string, int> connectedUsers = new Dictionary<string, int>();
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

        public async Task JoinRoom(string room, string user)
        {
            if (connectedUsers.ContainsKey(room))
            {
                if (connectedUsers[room] == 3)
                {
                    await Clients.Caller.SendAsync("ReceiveRandomMessage", $"{room} has reached to maximum limit. You can't join to this room!");
                    await Clients.Caller.SendAsync("RoomIsFull");
                    return;
                }
                else
                {
                    connectedUsers[room]+=1;
                }
            }
            else
            {
                connectedUsers.Add(room, 1);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.OthersInGroup(room).SendAsync("ReceiveJoinInfo", user);
        }

        public async Task LeaveRoom(string room, string user)
        {
            if (connectedUsers.ContainsKey(room))
            {
                connectedUsers[room] -= 1;
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.OthersInGroup(room).SendAsync("ReceiveLeaveInfo", user);
        }

        public async Task SendMessageRoom(string room, string user)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveInfoRoom", user, await fileService.Read(room));
        }

        public async Task EndBid(string user, double data)
        {
            await Clients.All.SendAsync("EndBid", user + " won the bid with ", data);
        }
    }
}
