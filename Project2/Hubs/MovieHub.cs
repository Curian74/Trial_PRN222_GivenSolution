using Microsoft.AspNetCore.SignalR;

namespace Project2.Hubs
{
    public class MovieHub: Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
