using OrderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Interfaces
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetail>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDetail>> GetDetailsByOrderIdAsync(int orderId);
    }
}
