using Inventory.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Thread = Inventory.Domain.Model.Thread;

namespace Inventory.Infractracture.DbConfiguration.EntityFramework;
public class InventoryWriteContext : DbContext
{
    internal DbSet<Post> Posts { get; set; }
    internal DbSet<Thread> Threads { get; set; }
    internal DbSet<Comment> Comments { get; set; }
    internal DbSet<Vote> Votes { get; set; }



    public InventoryWriteContext(DbContextOptions<InventoryWriteContext> dbContextOptions) : base(dbContextOptions)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(builder =>
        {
            builder.HasKey(p => p.Name);

            builder.Property(w => w.UserName)
                   .IsRequired();
            builder.Property(w => w.Description)
                    .IsRequired();
            builder.Property(w => w.FileCompressed);
                   
            builder.HasOne(p => p.Thread)
                   .WithMany(t => t.Posts)
                   .HasForeignKey("ThreadName")
                   .IsRequired();
        });

        modelBuilder.Entity<Thread>(builder =>
        {
            builder.HasKey(t => t.Name);

            builder.Property(w => w.Description)
                   .IsRequired();
        });

        modelBuilder.Entity<Comment>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.Property(w => w.Text)
                   .IsRequired();
            builder.HasOne(p => p.Post)
                   .WithMany(t => t.Comments)
                   .HasForeignKey("PostName")
                   .IsRequired();
        });


        modelBuilder.Entity<Vote>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.Property(w => w.VoteType)
                   .IsRequired();
            builder.Property(w => w.UserName)
                   .IsRequired();
            builder.HasOne(p => p.Post)
                   .WithMany(t => t.Votes)
                   .HasForeignKey("PostName")
                   .IsRequired();
        });
    }
}
