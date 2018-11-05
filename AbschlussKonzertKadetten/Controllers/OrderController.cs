using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Entity;
using AbschlussKonzertKadetten.Interface;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AbschlussKonzertKadetten.Controllers
{
    [Route("api/Order")]
    [Authorize]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly KadettenContext _context;
        private readonly IOrderRepo _orderRepo;
        private readonly IClientRepo _clientRepo;
        private readonly ITicketOrderRepo _ticketOrderRepo;
        private readonly ITicketRepo _ticketRepo;
        private readonly IKadettRepo _kadettRepo;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;

        public OrderController(KadettenContext context, IOrderRepo orderRepo, IClientRepo clientRepo,
            ITicketOrderRepo ticketOrderRepo, ITicketRepo ticketRepo, IKadettRepo kadettRepo, ILogger<OrderController> logger
            , IUserRepo userRepo, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _context = context;
            _orderRepo = orderRepo;
            _ticketOrderRepo = ticketOrderRepo;
            _clientRepo = clientRepo;
            _ticketRepo = ticketRepo;
            _userRepo = userRepo;
            _kadettRepo = kadettRepo;
            _emailSenderService = emailSenderService;
        }

        // GET api/values
        [HttpGet]
        public async Task<List<ViewModelOrder>> Get()
        {
            _logger.LogInformation("l items");
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
                        Quantity = orderTicket.Quantity,
                        Day = orderTicket.Day
                    };
                    modelTickets.Add(vmTicket);
                }

                var vm = new ViewModelOrder
                {
                    Email = client.Email,
                    Phone = client.Phone,
                    Bemerkung = order.Bemerkung,
                    ClientFirstName = client.FirstName,
                    ClientLastName = client.LastName,
                    Tickets = modelTickets,
                    KadettFirstName = kadett.FirstName,
                    KadettLastName = kadett.LastName,
                    KadettInKader = kadett.KadettInKader
                };
                modelOrders.Add(vm);
            }


            return modelOrders;
        }

        //GET api/values/5
        [HttpGet("{email}")]
        public async Task<ViewModelOrder> Get(string email)
        {
            _logger.LogInformation("Getting item {ID}", email);

            var client = await _clientRepo.ClientFindByEmail(email);
            var order = await _orderRepo.GetOrderByClientId(client.Id);

            var kadett = await _kadettRepo.GetKadettById(order.KadettId);
            var modelTickets = new List<ViewModelTicket>();
            var orderTickets = await _ticketOrderRepo.GetTicketOrderByOrderId(order.Id);

            foreach (var orderTicket in orderTickets)
            {
                var tickets = await _ticketRepo.GetTicketById(orderTicket.TicketId);
                var vmTicket = new ViewModelTicket()
                {
                    Type = tickets.Type,
                    Quantity = orderTicket.Quantity,
                    Day = orderTicket.Day
                };
                modelTickets.Add(vmTicket);
            }

            var modelOrder = new ViewModelOrder
            {
                Email = client.Email,
                Phone = client.Phone,
                Bemerkung = order.Bemerkung,
                ClientFirstName = client.FirstName,
                ClientLastName = client.LastName,
                Tickets = modelTickets,
                KadettFirstName = kadett.FirstName,
                KadettLastName = kadett.LastName,
                KadettInKader = kadett.KadettInKader
            };
            return modelOrder;
        }

        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post(ViewModelOrder order)
        {
            _logger.LogInformation("Post Order", order);

            if (ModelState.IsValid && order.Botfield == null)
            {
                var findClient = _clientRepo.ClientFindByEmail(order.Email).Result;
                if (findClient == null)
                {
                    var createClient = await _clientRepo.CreateClient(new Client()
                    {
                        Email = order.Email,
                        Phone = order.Phone,
                        LastName = order.ClientLastName,
                        FirstName = order.ClientFirstName
                    });
                    var createKadett = await _kadettRepo.CreateKadett(new Kadett()
                    {
                        LastName = order.KadettLastName,
                        FirstName = order.KadettFirstName,
                        KadettInKader = order.KadettInKader
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
                        Ticket ticketMatch = await _ticketRepo.GetByType(ticket.Type);
                        if (ticketMatch == null)
                            return BadRequest();

                        var ticketOrder = await _ticketOrderRepo.CreateTicketOrder(new TicketOrder()
                        {
                            Order = createOrder,
                            Ticket = ticketMatch,
                            Quantity = ticket.Quantity,
                            Day = ticket.Day
                        });
                    }
                    
                    //await _emailSenderService.SendEmailAsync(order.Email);
                    if(_clientRepo.ClientFindByEmail(order.Email).Result == null)
                        await _context.SaveChangesAsync();

                    return Ok();
                }
                return Conflict();
            }
            return ValidationProblem();
        }

        // PUT api/values/5
        [HttpPut("{email}")]
        public async Task<IActionResult> Put(string email, ViewModelUpdateOrder order)
        {
            _logger.LogInformation("Update Order", order, email);
            if (_clientRepo.ClientFindByEmail(email) != null)
            {
                var dbClient = await _clientRepo.ClientFindByEmail(email);
                var dbOrder = await _orderRepo.GetOrderByClientId(dbClient.Id);
                var dbKadett = await _kadettRepo.GetKadettById(dbOrder.KadettId);
                var dbTicketOrders = await _ticketOrderRepo.GetTicketOrderByOrderId(dbOrder.Id);

                if (order == null)
                {
                    return NotFound();
                }

                if (order.Email != dbClient.Email)
                    dbClient.Email = order.Email;

                foreach (var ticket in order.Tickets)
                {
                    var ticketMatch = await _ticketRepo.GetByType(ticket.Type);
                    if (ticketMatch == null)
                        return BadRequest();

                    foreach (var dbTicketOrder in dbTicketOrders)
                    {
                        if (dbTicketOrder.Ticket.Type == ticket.Type && dbTicketOrder.Day == ticket.Day)
                        {
                            dbTicketOrder.Quantity = ticket.Quantity;
                            dbTicketOrder.Day = ticket.Day;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        //DELETE api/values/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            _logger.LogInformation("Delete Order", email);

            var dbClient = await _clientRepo.ClientFindByEmail(email);
            var dbOrder = await _orderRepo.GetOrderByClientId(dbClient.Id);

            _clientRepo.DeleteClient(dbOrder.ClientId);
            _kadettRepo.DeleteKadett(dbOrder.KadettId);
            _orderRepo.DeleteOrder(dbOrder.Id);
            _ticketOrderRepo.DeleteOrderTicket(dbOrder.Id);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
