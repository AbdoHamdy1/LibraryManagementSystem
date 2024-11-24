using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace LMS.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet< Author> Authors{ get; set; }
        public DbSet<Publisher> publishers { get; set; }
        public DbSet<BookUser> bookUsers { get; set; }
    }
}
