﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByClientId(int id);
        void DeleteOrder(int id);
    }
}