using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IClientRepo
    {
        Task<Clients> CreateClient(Clients client);
        Task<Clients> GetClientById(int id);
        Task<Clients> ClientFindByEmail(string email);
        void DeleteClient(int id);

    }
}
