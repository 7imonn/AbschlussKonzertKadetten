using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Entity;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IRedactorRepo
    {
        Task<Redactor> GetReactorByNameAsync(string name);
        Task<IEnumerable<Redactor>> GetReactorAll();
        Task<Redactor> CreateRedactor(ViewModelRedactor redactor);
    }
}
