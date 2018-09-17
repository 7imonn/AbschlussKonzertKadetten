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

        public async Task<Client> CreateClient(Client client)
        {
            await _context.Client.AddAsync(client);
            return client;
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _context.Client.FindAsync(id);
        }

        public async Task<Client> ClientFindByEmail(string email)
        {
            return await _context.Client.FindAsync(email);
        }

        public async void DeleteClient(int id)
        {
            var deleteClient = await GetClientById(id);
            _context.Remove(deleteClient);
        }
    }
}
