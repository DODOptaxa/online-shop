using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.Identity
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Comment>(action =>
            {
                action.Property(c => c.Content).
                IsRequired()
                .HasMaxLength(305);

                action.Property(c => c.CreatedAt).IsRequired();


                action.Property(c => c.BookId).IsRequired();



            });

            builder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId) 
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}