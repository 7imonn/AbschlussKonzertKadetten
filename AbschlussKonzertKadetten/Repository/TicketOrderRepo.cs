using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;

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
    }
}
