﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;

namespace AbschlussKonzertKadetten.Repository
{
    public interface ITicketOrderRepo
    {
        Task<TicketOrder> CreateTicketOrder(TicketOrder ticketOrder);
        Task<List<TicketOrder>> GetTicketOrderByOrderId(int id);
        void DeleteOrderTicket(int id);
    }
}
