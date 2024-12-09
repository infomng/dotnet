using Entities;
using Microsoft.EntityFrameworkCore;

namespace Transactional_outbox_pattern.src.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Order> Orders { get; set; }
    public required DbSet<Outbox> Outbox { get; set; }
}