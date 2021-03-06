﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Interface;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        //[HttpGet]
        //public async Task<List<ViewModelRedactor>> Get()
        //{
        //    _logger.LogInformation("Delete All Order");

        //    var redactors = await _redactorRepo.GetReactorAll();
        //    var vmList = new List<ViewModelRedactor>();
        //    foreach (var redactor in redactors)
        //    {

        //        var vm = new ViewModelRedactor()
        //        {
        //            Name = redactor.Name,
        //            Text = redactor.Text
        //        };
        //        vmList.Add(vm);
        //    }

        //    return vmList;
        //}
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<ViewModelRedactor> Get(string name)
        {
            _logger.LogInformation("Get Redactor by name:", name);

            var redactor = await _redactorRepo.GetReactorByNameAsync(name);

            var vm = new ViewModelRedactor()
            {
                Name = redactor.Name,
                Text = redactor.Text
            };

            return vm;
        }
        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<bool> Get()
        {
            _logger.LogInformation("Get active");

            var isFormulaActive = await _formularActiveRepo.isActive();

            return isFormulaActive.Active;
        }
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
                _logger.LogInformation("Update Redactor", models);

                foreach (var model in models)
                {
                    var redactor = _redactorRepo.GetReactorByNameAsync(model.Name);
                    if (redactor.Result != null)
                    {
                        var dbReactor = await _redactorRepo.GetReactorByNameAsync(model.Name);

                        if (model.Text == null)
                            return NotFound();

                        dbReactor.Text = model.Text;

                        await _context.SaveChangesAsync();
                        return Ok();

                    }
                    else
                    {
                        await _redactorRepo.CreateRedactor(model);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
            }

            return ValidationProblem();
        }
    }
}