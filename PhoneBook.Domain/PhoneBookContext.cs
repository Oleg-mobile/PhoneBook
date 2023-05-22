using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Domain
{
    public class PhoneBookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public PhoneBookContext(DbContextOptions options) : base(options)
        {
        }
    }
}
