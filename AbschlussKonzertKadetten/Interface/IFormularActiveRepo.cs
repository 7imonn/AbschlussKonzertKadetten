using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Entity;

namespace AbschlussKonzertKadetten.Interface
{
    public interface IFormularActiveRepo
    {
        Task<FormularActive> isActive();
    }
}
