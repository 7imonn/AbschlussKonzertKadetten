using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace AbschlussKonzertKadetten.Controllers
{
    [Route("api/Redactor")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RedactorController : Controller
    {
        private readonly KadettenContext _context;
        private readonly ILogger _logger;
        private readonly IRedactorRepo _redactorRepo;
        public RedactorController(KadettenContext context, IRedactorRepo redactorRepo, ILogger<RedactorController> logger)
        {
            _context = context;
            _logger = logger;
            _redactorRepo = redactorRepo;

        }
        // GET: Redactor
        [HttpGet("{name}")]
        public async Task<ViewModelRedactor> Get(string name)
        {
            _logger.LogInformation("Delete All Order");

            var redactor = await _redactorRepo.GetReactorByNameAsync(name);

            var vm = new ViewModelRedactor()
            {
                Name = redactor.Name,
                Text = redactor.Text
            };

            return vm;
        }

        // GET: Redactor/Edit/5
        [HttpPut]
        public async Task<IActionResult> Put(ViewModelRedactor model)
        {
            _logger.LogInformation("Update Order", model);
            var redactor = _redactorRepo.GetReactorByNameAsync(model.Name);
            if (redactor.Result != null)
            {
                var dbReactor = await _redactorRepo.GetReactorByNameAsync(model.Name);

                if (model.Text == null)
                    return NotFound();

                dbReactor.Name = model.Name;
                dbReactor.Text = model.Text;

                await _context.SaveChangesAsync();
                return Ok();
            }

            if (ModelState.IsValid)
            {
                await _redactorRepo.CreateRedactor(model);
                await _context.SaveChangesAsync();
                return Ok();

            }
            return ValidationProblem();
        }
    }
}