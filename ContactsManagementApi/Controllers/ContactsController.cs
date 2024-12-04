using ContactsManagementApi.Models;
using ContactsManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsRepository _repository;

        public ContactsController()
        {
            _repository = new ContactsRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllContacts());
        }



        [HttpPost]
        public IActionResult Add([FromBody] Contact contact)
        {
            return Ok(_repository.AddContact(contact));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Contact contact)
        {
            return Ok(_repository.UpdateContact(contact));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteContact(id);
            return NoContent();
        }
    }
}
