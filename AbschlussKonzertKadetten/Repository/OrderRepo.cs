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
        public async Task<IEnumerable<Orders>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Orders> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }
        public async Task<Orders> GetOrdersByEmail(string email)
        {
            return await _context.Orders.FindAsync(email);
        }

        public async Task<Orders> GetOrderByClientId(int id)
        {
            var Orders = await _context.Orders.SingleOrDefaultAsync(o => o.ClientId == id);
            return Orders;
        }
        public async void DeleteOrder(int id)
        {
            var deleteOrders = await GetOrderById(id);
            _context.Orders.Remove(deleteOrders);
        }

        public async Task<Orders> CreateOrder(Orders Orders)
        {
            await _context.Orders.AddAsync(Orders);
            return Orders;
        }
    }
}
    