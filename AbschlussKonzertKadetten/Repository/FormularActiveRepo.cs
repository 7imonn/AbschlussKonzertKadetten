using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Entity;
using AbschlussKonzertKadetten.Interface;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class FormularActiveRepo : IFormularActiveRepo
    {
        private readonly KadettenContext _context;
            
        public FormularActiveRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<FormularActive> isActive()
        {
            var isActive = await _context.FormularActive.FirstOrDefaultAsync();
            return isActive;
        }

    }
}
