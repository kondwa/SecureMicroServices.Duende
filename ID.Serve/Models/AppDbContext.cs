using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ID.Serve.Models;

namespace ID.Serve.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<ID.Serve.Models.UserCredentialsDto> UserCredentialsDto { get; set; } = default!;
        public DbSet<ID.Serve.Models.UserRegisterDto> UserRegisterDto { get; set; } = default!;
        public DbSet<ID.Serve.Models.ExceptionDto> ExceptionDto { get; set; } = default!;
    }
}
