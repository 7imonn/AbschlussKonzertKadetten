using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class TicketRepo : ITicketRepo
    {
        private readonly KadettenContext _context;
        public TicketRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ticket>> GetTicketvariation()
        {
            return await _context.Ticket.ToListAsync();
        }
    }
}
