using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AbschlussKonzertKadetten.Controllers
{
    [Route("api/Ticket")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly KadettenContext _context;
        private readonly IOrderRepo _orderRepo;
        private readonly IClientRepo _clientRepo;
        private readonly ITicketOrderRepo _ticketOrderRepo;
        public OrderController(KadettenContext context, IOrderRepo orderRepo, IClientRepo clientRepo, ITicketOrderRepo ticketOrderRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
            _ticketOrderRepo = ticketOrderRepo;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await _orderRepo.GetAll();
        }

        //GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<Order> Get(int id)
        //{
        //    var Order = _context.Order.Find(id);
        //    if (Order == null)
        //    {
        //        return NotFound();
        //    }
        //    return Order;
        //}

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] ViewModelOrder order)
        {
            if (ModelState.IsValid)
            {
                if (_clientRepo.ClientFindByEmail(order.Email) == null)
                {
                    var createClient = await _clientRepo.CreateClient(new Client()
                    {
                        Email = order.Email,
                        LastName = order.LastName,
                        FirstName = order.FirstName
                    });

                    var createOrder = await _orderRepo.CreateOrder(new Order()
                    {
                        Bemerkung = order.Bemerkung,
                        OrderDate = DateTime.Now,
                        Clients = createClient
                    });
                    var ticketOrder = await _ticketOrderRepo.CreateTicketOrder(new TicketOrder()
                    {
                        Order = createOrder,
                        
                    });
                }
            }

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
