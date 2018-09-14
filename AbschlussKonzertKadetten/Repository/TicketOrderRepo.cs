using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class TicketOrderRepo : ITicketOrderRepo
    {
        private readonly KadettenContext _context;
        public TicketOrderRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<TicketOrder> CreateTicketOrder(TicketOrder ticketOrder)
        {
            await _context.TicketOrders.AddAsync(ticketOrder);
            return ticketOrder;
        }

        public Task<List<TicketOrder>> GetTicketOrderByOrderId(int id)
        {
            var list = _context.TicketOrders.Where(to => to.OrderId.Equals(id));
            return list.ToListAsync();
        }
    }
}
