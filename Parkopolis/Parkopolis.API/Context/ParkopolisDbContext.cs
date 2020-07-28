using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Context
{
    public class ParkopolisDbContext : IdentityDbContext
    {
        public ParkopolisDbContext(DbContextOptions<ParkopolisDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().ToTable("User");
            model.Entity<Area>().ToTable("Area");
            model.Entity<City>().ToTable("City");
            model.Entity<ParkingLot>().ToTable("ParkingLot");
            model.Entity<ParkingSpace>().ToTable("ParkingSpaces");
            base.OnModelCreating(model);
        }
    }
}
