using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Entity;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;

namespace AbschlussKonzertKadetten.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly KadettenContext _context;
        private readonly IOrderRepo _orderRepo;
        private readonly IClientRepo _clientRepo;
        private readonly ITicketOrderRepo _ticketOrderRepo;
        private readonly ITicketRepo _ticketRepo;
        private readonly IKadettRepo _kadettRepo;
        public OrderController(KadettenContext context, IOrderRepo orderRepo, IClientRepo clientRepo, ITicketOrderRepo ticketOrderRepo, ITicketRepo ticketRepo, IKadettRepo kadettRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
            _ticketOrderRepo = ticketOrderRepo;
            _clientRepo = clientRepo;
            _ticketRepo = ticketRepo;
            _kadettRepo = kadettRepo;
        }
        // GET api/values
        [HttpGet]
        public async Task<List<ViewModelOrder>> Get()
        {
            var orderList = await _orderRepo.GetAllOrders();
            var modelOrders = new List<ViewModelOrder>();

            foreach (var order in orderList)
            {
                var client = await _clientRepo.GetClientById(order.ClientId);
                var kadett = await _kadettRepo.GetKadettById(order.KadettId);
                var modelTickets = new List<ViewModelTicket>();
                var orderTickets = await _ticketOrderRepo.GetTicketOrderByOrderId(order.Id);

                foreach (var orderTicket in orderTickets)
                {
                    var tickets = await _ticketRepo.GetTicketById(orderTicket.TicketId);
                    var vmTicket = new ViewModelTicket()
                    {
                        Type = tickets.Type,
                        Quantity = orderTicket.Quantity
                    };
                    modelTickets.Add(vmTicket);
                }

                var vm = new ViewModelOrder
                {
                    Email = client.Email,
                    Bemerkung = order.Bemerkung,
                    ClientFirstName = client.FirstName,
                    ClientLastName = client.LastName,
                    Tickets = modelTickets,
                    KadettFirstName = kadett.FirstName,
                    KadettLastName = kadett.LastName
                };
                modelOrders.Add(vm);
            }


            return modelOrders;
        }

        //GET api/values/5
        [HttpGet("{id}")]
        public async Task<ViewModelOrder> Get(int id)
        {
            var orderList = await _orderRepo.GetAllOrders();
            var modelOrder = new ViewModelOrder();

            foreach (var order in orderList)
            {
                var client = await _clientRepo.GetClientById(order.ClientId);
                var kadett = await _kadettRepo.GetKadettById(order.KadettId);
                var modelTickets = new List<ViewModelTicket>();
                var orderTickets = await _ticketOrderRepo.GetTicketOrderByOrderId(order.Id);

                foreach (var orderTicket in orderTickets)
                {
                    var tickets = await _ticketRepo.GetTicketById(orderTicket.TicketId);
                    var vmTicket = new ViewModelTicket()
                    {
                        Type = tickets.Type,
                        Quantity = orderTicket.Quantity
                    };
                    modelTickets.Add(vmTicket);
                }

               modelOrder = new ViewModelOrder
                {
                    Email = client.Email,
                    Bemerkung = order.Bemerkung,
                    ClientFirstName = client.FirstName,
                    ClientLastName = client.LastName,
                    Tickets = modelTickets,
                    KadettFirstName = kadett.FirstName,
                    KadettLastName = kadett.LastName
                };
            }

            return modelOrder;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ViewModelOrder order)
        {
            if (ModelState.IsValid)
            {
                if (_clientRepo.ClientFindByEmail(order.Email) != null)
                {
                    var createClient = await _clientRepo.CreateClient(new Client()
                    {
                        Email = order.Email,
                        LastName = order.ClientLastName,
                        FirstName = order.ClientFirstName
                    });
                    var createKadett = await _kadettRepo.CreateKadett(new Kadett()
                    {
                        LastName = order.KadettLastName,
                        FirstName = order.KadettFirstName
                    });
                    var createOrder = await _orderRepo.CreateOrder(new Order()
                    {
                        Bemerkung = order.Bemerkung,
                        OrderDate = DateTime.Now,
                        Client = createClient,
                        Kadett = createKadett
                    });

                    foreach (var ticket in order.Tickets)
                    {

                        var ticketMatch = await _ticketRepo.GetByType(ticket.Type);
                        if (ticketMatch == null)
                            return BadRequest();

                        var ticketOrder = await _ticketOrderRepo.CreateTicketOrder(new TicketOrder()
                        {
                            Order = createOrder,
                            Ticket = ticketMatch,
                            Quantity = ticket.Quantity,
                            Day = ticket.Date
                        });
                    }

                    await _context.SaveChangesAsync();
                }
            }
            return Ok();

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ViewModelOrder value)
        {
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }


            _context.Order.Update(order);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
