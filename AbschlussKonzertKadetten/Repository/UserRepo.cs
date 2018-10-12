using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Entity;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly KadettenContext _context;
        public UserRepo(KadettenContext context)
        {
            _context = context;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _context.User.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
                return null;

            user.Password = null;
            return user;
        }
        public bool UserIsAuthenticate(string username, string password)
        {
            var user = Task.Run(() => _context.User.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
                return false;

            return true;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var Users = await _context.User.ToListAsync();
            foreach (var user in Users)
            {
                user.Password = null;
            }
            return Users;
        }
    }
}
