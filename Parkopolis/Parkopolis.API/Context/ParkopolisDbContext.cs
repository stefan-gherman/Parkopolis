using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Context
{
    public class ParkopolisDbContext : IdentityDbContext<ApplicationUser>
    {
        public ParkopolisDbContext(DbContextOptions<ParkopolisDbContext> options) : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public override DbSet<ApplicationUser> Users { get; set; }

        
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Area>().ToTable("Area");
            model.Entity<City>().ToTable("City");
            model.Entity<ParkingLot>().ToTable("ParkingLot");
            model.Entity<ParkingSpace>().ToTable("ParkingSpaces");
            base.OnModelCreating(model);
        }
    }
}
