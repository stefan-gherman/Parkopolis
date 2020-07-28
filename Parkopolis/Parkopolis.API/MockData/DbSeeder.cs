using Parkopolis.API.Context;
using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.MockData
{
    public class DbSeeder
    {
        public static void Init (ParkopolisDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cities.Any() && context.Areas.Any() && context.ParkingLots.Any())
            {
                return;
            }

            var owners = new UserViewModel[]
            {
                new UserViewModel{FirstName="Bartolomeu", LastName="Petrescu", 
                    Password="k34", PhoneNumber="07345678", Email="bp@mail.com", UserName="bpetre44", Type=UserType.Owner},
                new UserViewModel{FirstName="Lucretiu", LastName="Popa",
                    Password="k34", PhoneNumber="07345678", Email="lp@mail.com", UserName="lpopa42", Type=UserType.Owner},
                new UserViewModel{FirstName="Diocletian", LastName="Sandu",
                    Password="k34", PhoneNumber="07345678", Email="ds@mail.com", UserName="dsandu44", Type=UserType.Owner},
                new UserViewModel{FirstName="Primaria", LastName="Sector 2",
                    Password="k34", PhoneNumber="07345678", Email="primsect2@mail.com", UserName="ps2", Type=UserType.Owner},
                new UserViewModel{FirstName="SuperParking", LastName="Corp",
                    Password="k34", PhoneNumber="07345678", Email="spcorp@mail.com", UserName="sp2", Type=UserType.Owner},
                new UserViewModel{FirstName="Simple", LastName="User",
                    Password="k34", PhoneNumber="07345678", Email="spcorp@mail.com", UserName="sp2", Type=UserType.Customer},
                new UserViewModel{FirstName="Simple", LastName="Admin",
                    Password="k34", PhoneNumber="07345678", Email="spcorp@mail.com", UserName="sp2", Type=UserType.Admin}
            };

            context.Users.AddRange(owners);
            context.SaveChanges();


            var cities = new City[]
            {
                new City{ Name = "Bucharest"},
                new City {Name="Cluj-Napoca"},
                new City { Name="Craiova"}
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();


            var areas = new Area[]
            {
                new Area{Name="Piata Victoriei", CityId = 3},
                new Area{Name="Piata Romana", CityId = 3},
                new Area{Name="Gruia", CityId = 2},
                new Area{Name="Piata Centrala", CityId = 1}
            };

            context.Areas.AddRange(areas);
            context.SaveChanges();

            var parkingLots = new ParkingLot[]
            {
                new ParkingLot{ Name="Central Parking Victoriei", HasSecurity=true, IsPaid=true, 
                    IsStateOwned=false, Location="Near Kiselev Park", TotalParkingSpaces=27, AreaId=1, UserId=1},
                 new ParkingLot{ Name="Victoriei MegaParking", HasSecurity=true, IsPaid=true,
                    IsStateOwned=false, Location="In front of the government building", TotalParkingSpaces=21, AreaId=1, UserId=2},
                  new ParkingLot{ Name="Gruia Parking", HasSecurity=false, IsPaid=true,
                    IsStateOwned=false, Location="Near CFR Cluj Stadium", TotalParkingSpaces=50, AreaId=2, UserId=3}
            };

            context.ParkingLots.AddRange(parkingLots);
            context.SaveChanges();


            var parkingSpace = new ParkingSpace[]
            {
                new ParkingSpace{Name="a33", IsCovered=true, IsTaken=false, HasCarWash=true, Details="Near exit", ParkingLotId=1, Price=12.24M },
                new ParkingSpace{Name="a31", IsCovered=false, IsTaken=false, HasCarWash=false, Details="Near exit", ParkingLotId=1,  Price=12.24M},
                new ParkingSpace{Name="ty67", IsCovered=true, IsTaken=false, HasCarWash=true, Details="Near exit", ParkingLotId=2,  Price=12.24M },
                new ParkingSpace{Name="ty897", IsCovered=true, IsTaken=false, HasCarWash=true, Details="Near exit", ParkingLotId=2, Price=12.24M },
                new ParkingSpace{Name="k89", IsCovered=true, IsTaken=false, HasCarWash=true, Details="Near exit", ParkingLotId=3,  Price=12.24M },
                new ParkingSpace{Name="p89", IsCovered=true, IsTaken=false, HasCarWash=true, Details="Near exit", ParkingLotId=3,  Price=12.24M }
            };

            context.ParkingSpaces.AddRange(parkingSpace);
            context.SaveChanges();
 
         }
    }
}
