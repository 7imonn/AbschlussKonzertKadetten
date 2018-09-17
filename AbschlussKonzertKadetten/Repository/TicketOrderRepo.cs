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
            var list = _context.TicketOrders.Include(to => to.Ticket).Where(to => to.OrderId == id);
            return list.ToListAsync();
        }

        public void DeleteOrderTicket(int id)
        {
            List<TicketOrder> deleteOrderTickets = GetTicketOrderByOrderId(id).Result;
            foreach (var deleteOrderTicket in deleteOrderTickets)
            {
                _context.Remove(deleteOrderTicket);
            }
        }
    }
}
