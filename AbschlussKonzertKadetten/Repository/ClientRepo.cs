using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class ClientRepo : IClientRepo
    {
        private readonly KadettenContext _context;

        public ClientRepo(KadettenContext context)
        {
            _context = context;
        }

        public async Task<Clients> CreateClient(Clients Clients)
        {
            await _context.Clients.AddAsync(Clients);
            return Clients;
        }

        public async Task<Clients> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Clients> ClientFindByEmail(string email)
        {
            var Clients = await _context.Clients.SingleOrDefaultAsync(c => c.Email == email);
            return Clients;
        }

        public async void DeleteClient(int id)
        {
            var deleteClients = await GetClientById(id);
            _context.Remove(deleteClients);
        }
    }
}
