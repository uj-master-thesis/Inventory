using Inventory.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Thread = Inventory.Domain.Model.Thread;

namespace Inventory.Infractracture.DbConfiguration.EntityFramework;
public class InventoryWriteContext : DbContext
{
    internal DbSet<Post> Posts { get; set; }
    internal DbSet<Thread> Threads { get; set; }

    public InventoryWriteContext(DbContextOptions<InventoryWriteContext> dbContextOptions) : base(dbContextOptions)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(w => w.PostName)
                    .IsRequired();
            builder.Property(w => w.Email)
                   .IsRequired();
            builder.Property(w => w.Description)
                    .IsRequired();
            builder.Property(w => w.PostName)
                    .IsRequired();
            builder.HasOne(p => p.Thread)
                   .WithMany(t => t.Posts)
                   .HasForeignKey("ThreadId")
                   .IsRequired();
        });

        modelBuilder.Entity<Thread>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.Property(w => w.Name)
                   .IsRequired();
            builder.Property(w => w.Description)
                   .IsRequired();
        });
    }
}
