using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> CreateOrder(Order order);
    }
}
