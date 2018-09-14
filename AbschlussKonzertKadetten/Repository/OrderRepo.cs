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
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Order.AddAsync(order);
            return order;
        }
    }
}
    