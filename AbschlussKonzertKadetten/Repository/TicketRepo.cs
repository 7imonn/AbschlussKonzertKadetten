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
        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync();
        }

        public async Task<Ticket> GetByType(string type)
        {
            var lala = await _context.Ticket.SingleOrDefaultAsync(typ => typ.Type == type);
            return lala;
        }
        public async Task<Ticket> GetTicketById(int id)
        {
            var lala = await _context.Ticket.FindAsync(id);
            return lala;
        }
    }
}
