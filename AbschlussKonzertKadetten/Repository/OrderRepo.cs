using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly KadettenContext _context;
        public OrderRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Order.FindAsync(id);
        }

        public Task<Order> GetOrderByClientId(int id)
        {
            Task<Order> order = _context.Order.Where(o => o.ClientId == id).FirstOrDefaultAsync();
            return order;
        }
        public async void DeleteOrder(int id)
        {
            var deleteOrder = await GetOrderById(id);
            _context.Order.Remove(deleteOrder);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Order.AddAsync(order);
            return order;
        }
    }
}
    