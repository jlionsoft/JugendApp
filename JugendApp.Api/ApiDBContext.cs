using EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JugendApp.SharedModels;
using JugendApp.SharedModels.Person;
using JugendApp.SharedModels.Events;
using JugendApp.SharedModels.Groups;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using JugendApp.Api.Identity;
namespace JugendApp.Api
{
    public class ApiDBContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<InstrumentSkill> InstrumentSkills { get; set; }
        public DbSet<ContactOption> ContactOptions { get; set; }



        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }



        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Invitation> Invitations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Person)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Person <-> ContactOption : 1:n
            modelBuilder.Entity<Person>()
                .HasMany(p => p.ContactOptions)
                .WithOne(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Person <-> InstrumentSkill : 1:n (Aggregate Root = Person)
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Instruments)
                .WithOne(s => s.Person)
                .HasForeignKey(s => s.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // InstrumentSkill -> Instrument : n:1 (Instrument ist Lookup, optional keine Gegen-Navigation)
            modelBuilder.Entity<InstrumentSkill>()
                .HasOne(s => s.Instrument)
                .WithMany() // keine Collection in Instrument nötig
                .HasForeignKey(s => s.InstrumentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint: pro Person nur ein Eintrag pro Instrument
            modelBuilder.Entity<InstrumentSkill>()
                .HasIndex(s => new { s.PersonId, s.InstrumentId })
                .IsUnique();



            // Group <-> GroupMember : 1:n
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithOne(m => m.Group)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Person <-> GroupMember : 1:n (Person kann in mehreren Gruppen sein)
            //modelBuilder.Entity<Person>()
            //    .HasMany(p => p.GroupMemberships) 
            //    .WithOne(m => m.Person)
            //    .HasForeignKey(m => m.PersonId)
            //    .OnDelete(DeleteBehavior.Cascade);

            // Optional: Unique constraint — pro Person nur einmal pro Gruppe
            modelBuilder.Entity<GroupMember>()
                .HasIndex(m => new { m.GroupId, m.PersonId })
                .IsUnique();





            // Event -> Person (CreatedBy)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.CreatedBy)
                .WithMany() // optional: Person kann Events kennen, sonst WithMany()
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent event deletion when person deleted OR choose Cascade if desired

            // Event -> Location
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Location)
                .WithMany() // optional: Location kann Events kennen
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> Address (1:1 oder 1:n je Geschäftslogik; hier 1:1)
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Address)
                .WithOne()
                .HasForeignKey<Location>(l => l.AddressId)
                .OnDelete(DeleteBehavior.Cascade);


            // Feldlängen / Required
            modelBuilder.Entity<Event>().Property(e => e.Title).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Location>().Property(l => l.Name).HasMaxLength(200);
            modelBuilder.Entity<Address>().Property(a => a.PostalCode).HasMaxLength(20);



            modelBuilder.Entity<Invitation>()
                .HasKey(i => new { i.EventId, i.PersonId });

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Invitations)
                .HasForeignKey(i => i.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Person)
                .WithMany(p => p.Invitations)
                .HasForeignKey(i => i.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
