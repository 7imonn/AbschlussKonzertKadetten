using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Entity;
using AbschlussKonzertKadetten.Models;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class RedactorRepo : IRedactorRepo
    {
        private readonly KadettenContext _context;

        public RedactorRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<Redactor> GetReactorByNameAsync(string name)
        {
            var redactor = await _context.Redactor.SingleOrDefaultAsync(r => r.Name == name);
            return redactor;
        }

        public async Task<IEnumerable<Redactor>> GetReactorAll()
        {
            var redactor = await _context.Redactor.ToListAsync();
            return redactor;
        }

        public async Task<Redactor> CreateRedactor(ViewModelRedactor redactor)
        {
            var redactorDb = new Redactor()
            {
                Name = redactor.Name,
                Text = redactor.Text
            };
            await _context.Redactor.AddAsync(redactorDb);
            return redactorDb;
        }
    }
}
