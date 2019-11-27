using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Orders>> GetAllOrders();
        Task<Orders> CreateOrder(Orders order);
        Task<Orders> GetOrderById(int id);
        Task<Orders> GetOrderByClientId(int id);
        void DeleteOrder(int id);
    }
}
