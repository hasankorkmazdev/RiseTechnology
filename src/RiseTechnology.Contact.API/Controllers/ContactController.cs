using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Contact.API.Services;
using System;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            var data = await _contactService.GetPersonList();

            return Ok(data);
        }

        [HttpGet("{personuuid}")]
        public async Task<IActionResult> Get(Guid personuuid)
        {
            var data = await _contactService.GetPerson(personuuid);

            return Ok(data);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddPerson(CreatePersonRequestModel person)
        {
            await _contactService.CreatePersonAsync(person);
            return Ok();
        }

        [HttpPut("{personuuid}")]
        public async Task<IActionResult> Put(Guid personuuid)
        {
            await _contactService.RemovePersonAsync(personuuid);
            return Ok();
        }

        [HttpPost("{personuuid}/ContactInformation/")]
        public async Task<IActionResult> AddPersonContactInformation(Guid personuuid, AddPersonContactInformation addPersonContactInformation)
        {
            await _contactService.AddPersonContactInformation(personuuid, addPersonContactInformation);
            return Ok();
        }

        [HttpPut("{personuuid}/ContactInformation/{contactinformationuuid}")]
        public async Task<IActionResult> PutContactInformation(Guid personuuid, Guid contactinformationuuid)
        {
            await _contactService.RemoveContactInformation(personuuid, contactinformationuuid);
            return Ok();
        }

    }
}
