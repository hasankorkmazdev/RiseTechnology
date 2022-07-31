using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiseTechnology.Contact.API.Models;
using RiseTechnology.Contact.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IContactService _contactService;

        public PersonController(ILogger<PersonController> logger,IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreatePerson person)
        {
            await _contactService.CreatePersonAsync(person);
            return Ok();
        }
    }
}
