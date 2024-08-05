using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.DBContext
{
    public class CarRentalContext :DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
                
        }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Cars> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.UserName)
                    .IsRequired();

                entity.Property(e => e.MobileNumber)
                    .IsRequired();

                entity.Property(e => e.Comments)
                    .IsRequired(false);

                entity.Property(e => e.From)
                    .IsRequired();

                entity.Property(e => e.To)
                    .IsRequired();
                // Explain Why I Choose this Relation type ? 
                // i set the relation (one to many) because we can create multiple orders on the same cars .
                // also, in the feuture if we want to control on the date of rent to prevent place order for the same car at the same date .
                // we should leave the relation as this because we can place more than one order on the same car at different dates .
                entity.HasOne(e => e.Car)
                    .WithMany()  
                    .HasForeignKey(e => e.CarID)
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.ID); 

                entity.Property(e => e.Name)
                    .IsRequired(); 

            });
        }
    }
}
