using IPtor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPtor.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Tire> Tires { get; set; }
        public DbSet<Defects> Defects { get; set; }
        public DbSet<DefectsTO> DefectsTOs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<RepairRequest> RepairRequests { get; set; } // добавлено после
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //настройка связей(Fluent API)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Battery>()
                .HasOne(b => b.Car) // У одной батареи — одна машина
                .WithMany(c => c.Batteries) // У одной машины — много батарей
                .HasForeignKey(b => b.CarId) // Внешний ключ — CarId в таблице Batteries
                .OnDelete(DeleteBehavior.Cascade); // При удалении машины — удалить все её батареи

            modelBuilder.Entity<Tire>()
                .HasOne(t => t.Car) // У одной шины — одна машина
                .WithMany(c => c.Tires) // У одной машины — много шин
                .HasForeignKey(t => t.CarId) // Внешний ключ — CarId в таблице Tires
                .OnDelete(DeleteBehavior.Cascade); // При удалении машины — удалить все её шины

            modelBuilder.Entity<Defects>()
                .HasOne(d => d.Car) // У одного акта дефектовки — одна машина
                .WithMany(c => c.Defects) // У одной машины — много актов дефектовки
                .HasForeignKey(d => d.CarId) // Внешний ключ — CarId в таблице Defects
                .OnDelete(DeleteBehavior.Cascade); // При удалении машины — удалить все акты дефектовки

            modelBuilder.Entity<DefectsTO>()
                .HasOne(d => d.Car) // У одного акта тех. состояния — одна машина
                .WithMany(c => c.DefectsTO) // У одной машины — много актов тех. состояния
                .HasForeignKey(d => d.CarId) // Внешний ключ — CarId в таблице DefectsTO
                .OnDelete(DeleteBehavior.Cascade); // При удалении машины — удалить все акты тех. состояния

            modelBuilder.Entity<RepairRequest>() //добавлено после
                .HasOne(r => r.Car)
                .WithMany(c => c.RepairRequests)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.NoAction);
            //дополнительные связи для восстановления актов
            modelBuilder.Entity<Defects>()
                .HasOne(d => d.SheffPassService)
                .WithMany()
                .HasForeignKey(d => d.SheffPassServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Defects>()
                .HasOne(d => d.ResponcePerson)
                .WithMany()
                .HasForeignKey(d => d.ResponcePersonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Defects>()
                .HasOne(d => d.Driver)
                .WithMany()
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Defects>()
                .HasOne(d => d.AcceptUtoPerson)
                .WithMany()
                .HasForeignKey(d => d.AcceptUtoPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Defects>()
                .HasOne(d => d.AdditionalPerson)
                .WithMany()
                .HasForeignKey(d => d.AdditionalPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DefectsTO>()
                .HasOne(d => d.SheffPassService)
                .WithMany()
                .HasForeignKey(d => d.SheffPassServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DefectsTO>()
                .HasOne(d => d.ResponcePerson)
                .WithMany()
                .HasForeignKey(d => d.ResponcePersonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DefectsTO>()
                .HasOne(d => d.Driver)
                .WithMany()
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
