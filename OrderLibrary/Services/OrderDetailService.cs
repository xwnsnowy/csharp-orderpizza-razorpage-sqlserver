using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using OrderLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrdersAsync()
        {
            return await _orderDetailRepository.GetAllAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetDetailsByOrderIdAsync(int orderId)
        {
            return await _orderDetailRepository.GetDetailsByOrderIdAsync(orderId);
        }
    }
}
