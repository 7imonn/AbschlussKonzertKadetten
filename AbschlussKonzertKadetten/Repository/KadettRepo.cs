using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Entity;

namespace AbschlussKonzertKadetten.Repository
{
    public class KadettRepo : IKadettRepo
    {
        private readonly KadettenContext _context;

        public KadettRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<Kadett> GetKadettById(int id)
        {
            var kadett = await _context.Kadett.FindAsync(id);
            return kadett;
        }

        public async Task<Kadett> CreateKadett(Kadett kadett)
        {
            await _context.Kadett.AddAsync(kadett);
            return kadett;
        }

        public async void DeleteKadett(int id)
        {
            var deleteKadett = await GetKadettById(id);
            _context.Remove(deleteKadett);
        }
    }
}
