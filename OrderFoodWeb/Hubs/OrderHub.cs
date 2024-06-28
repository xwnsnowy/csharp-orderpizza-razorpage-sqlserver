using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrderFoodWeb.Hubs
{
    public class OrderHub : Hub
    {
        public async Task SendOrderUpdate(string orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", orderId, status);
        }
    }
}
