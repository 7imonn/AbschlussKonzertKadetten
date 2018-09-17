using AbschlussKonzertKadetten.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IKadettRepo
    {
        Task<Kadett> GetKadettById(int id);
        Task<Kadett> CreateKadett(Kadett kadett);
    }
}
