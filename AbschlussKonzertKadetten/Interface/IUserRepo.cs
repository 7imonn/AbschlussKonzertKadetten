using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Entity;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IUserRepo
    {
        Task<User> Authenticate(string username, string password);
        bool UserIsAuthenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
