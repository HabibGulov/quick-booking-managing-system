using Microsoft.EntityFrameworkCore;

public class QuickBookingDbContext : DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }

    public QuickBookingDbContext(DbContextOptions<QuickBookingDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>()
            .HasAlternateKey(w => w.Email);

        modelBuilder.Entity<Client>()
            .HasAlternateKey(c => c.Email);
    }
}