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
            IQueryable<Contact> query = _context.Contacts;
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(c =>
                    c.Name.Trim().ToLower().Contains(key.Trim().ToLower()) ||
                    c.Surname.Trim().ToLower().Contains(key.Trim().ToLower()) ||
                    c.Phone.Trim().ToLower().Contains(key.Trim().ToLower()) ||
                    c.Email.Trim().ToLower().Contains(key.Trim().ToLower()));
            }
            var contacts = query.ToList();
            return Ok(contacts);
        }

        [HttpPost("[action]")]
        public IActionResult Create(Contact contact)
        {
            var item = _context.Contacts.FirstOrDefault(i => i.Name == contact.Name && i.Surname == contact.Surname);

            if (item is not null)
            {
                return BadRequest($"Контакт {contact.Name} {contact.Surname} уже существует");
            }

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact is null)
            {
                return BadRequest($"Контакт с Id = {id} не существует");
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Update(Contact contact)
        {
            var item = _context.Contacts.FirstOrDefault(i => i.Id == contact.Id);

            if (item is null)
            {
                return BadRequest($"Контакт с Id = {contact.Id} не существует");
            }

            item.Name = contact.Name;
            item.Surname = contact.Surname;
            item.Phone = contact.Phone;
            item.Email = contact.Email;

            _context.Contacts.Update(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Get(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(i => i.Id == id);
            if (contact is null)
            {
                return BadRequest($"Контакт с Id = {id} не существует");
            }
            return Ok(contact);
        }
    }
}
