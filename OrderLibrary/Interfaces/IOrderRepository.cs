using OrderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
