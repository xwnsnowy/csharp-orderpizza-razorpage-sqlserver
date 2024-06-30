using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrderFoodWeb.Hubs
{
    public class CartHub : Hub
    {
        public async Task SendCartCount(int count)
        {
            await Clients.All.SendAsync("ReceiveCartCount", count);
        }
    }
}
