using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain;

namespace PhoneBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly PhoneBookContext _context;

        public ContactsController(PhoneBookContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll(string? key = null)
        {
            var contacts = _context.Contacts.ToList();
            if (!string.IsNullOrEmpty(key))
            {
                contacts = contacts.Where(c =>
                    c.Name.Trim().ToLower().Contains(key.Trim().ToLower()) ||
                    c.Surname.Trim().ToLower().Contains(key.Trim().ToLower())).ToList();
            }
            return Ok(contacts);
        }

        [HttpPost("[action]")]
        public IActionResult Create(Contact contact)
        {
            var item = _context.Contacts.FirstOrDefault(i => i.Name == contact.Name && i.Surname == contact.Surname);

            if (item is null)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact is not null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Update(int id, Contact contact)
        {
            var item = _context.Contacts.FirstOrDefault(i => i.Id == id);

            if (item is not null)
            {
                item.Name = contact.Name;
                item.Surname = contact.Surname;
                item.Phone = contact.Phone;
                item.Email = contact.Email;

                _context.Contacts.Update(item);
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}
