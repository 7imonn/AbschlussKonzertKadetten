using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbschlussKonzertKadetten.Controllers
{
    [Route("api/Ticket")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly KadettenContext _context;
        public OrderController(KadettenContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return _context.Order.ToList();
        }

        //GET api/values/5
        //[HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var Order = _context.Order.Find(id);
            if (Order == null)
            {
                return NotFound();
            }
            return Order;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] ViewModelOrder order)
        {
            
            _context.Order.Add(new Order()
            {
                Bemerkung = order.Bemerkung,
                OrderDate = DateTime.Now,
                Clients = new Client()
                {
                    Email = order.Email,
                    FirstName = order.FirstName,
                    LastName = order.LastName
                }
            });

            _context.SaveChanges();
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

            //order.Bemerkung = value.Email;
            //order.Clients = value.FirstName;
            //order.Tickets = value.LastName;

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
