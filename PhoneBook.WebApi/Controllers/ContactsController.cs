using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetAll2()
        {
            return Ok();
        }
    }
}
