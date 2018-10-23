using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Interface;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace AbschlussKonzertKadetten.Controllers
{
    [Route("api/redactor")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RedactorController : Controller
    {
        private readonly KadettenContext _context;
        private readonly ILogger _logger;
        private readonly IRedactorRepo _redactorRepo;
        private readonly IFormularActiveRepo _formularActiveRepo;
        public RedactorController(KadettenContext context, IRedactorRepo redactorRepo, IFormularActiveRepo formularActiveRepo, ILogger<RedactorController> logger)
        {
            _context = context;
            _logger = logger;
            _redactorRepo = redactorRepo;
            _formularActiveRepo = formularActiveRepo;

        }
        [HttpGet]
        public async Task<List<ViewModelRedactor>> Get()
        {
            _logger.LogInformation("Delete All Order");

            var redactors = await _redactorRepo.GetReactorAll();
            var vmList = new List<ViewModelRedactor>();
            foreach (var redactor in redactors)
            {

                var vm = new ViewModelRedactor()
                {
                    Name = redactor.Name,
                    Text = redactor.Text
                };
                vmList.Add(vm);
            }

            return vmList;
        }
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
        //[HttpGet("active")]
        //public async Task<bool> Get()
        //{
        //    _logger.LogInformation("Delete All Order");

        //    var isFormulaActive = await _formularActiveRepo.isActive();

        //    return isFormulaActive.Active;
        //}
        [HttpPut("active/{active}")]
        public async Task<IActionResult> Put(bool active)
        {
            _logger.LogInformation("Delete All Order");

            var dbActive = _formularActiveRepo.isActive();
            dbActive.Result.Active = active;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: Redactor/Edit/5
        [HttpPut]
        public async Task<IActionResult> Put(List<ViewModelRedactor> models)
        {
            if (ModelState.IsValid)
            {
                foreach (var model in models)
                {

                    _logger.LogInformation("Update Redactor", model);
                    var redactor = _redactorRepo.GetReactorByNameAsync(model.Name);
                    if (redactor.Result != null)
                    {
                        var dbReactor = await _redactorRepo.GetReactorByNameAsync(model.Name);

                        if (model.Text == null)
                            return NotFound();

                        dbReactor.Name = model.Name;
                        dbReactor.Text = model.Text;

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _redactorRepo.CreateRedactor(model);
                        await _context.SaveChangesAsync();
                    }

                }
                return Ok();
            }

            return ValidationProblem();
        }
    }
}