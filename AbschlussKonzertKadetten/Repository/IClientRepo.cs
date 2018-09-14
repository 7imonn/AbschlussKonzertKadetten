using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IClientRepo
    {
        Task<Client> CreateClient(Client client);
        Task<Client> GetClientById(int id);
        Task<Client> ClientFindByEmail(string email);
    }
}
