﻿using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Utils
{
    public static class CopyClass
    {
        public static void CopyParkingLot(ParkingLot source, ParkingLot destination)
        {
            destination.AreaId = source.AreaId;
            destination.HasSecurity = source.HasSecurity;
            destination.IsPaid = source.IsPaid;
            destination.Location = source.Location;
            destination.TotalParkingSpaces = source.TotalParkingSpaces;
            destination.IsStateOwned = source.IsStateOwned;
            destination.Name = source.Name;
            destination.ApplicationUserId = source.ApplicationUserId;
        }

        public static void CopyParkingSpace(ParkingSpace source, ParkingSpace destination)
        {
            destination.Details = source.Details;
            destination.HasCarWash = source.HasCarWash;
            destination.IsCovered = source.IsCovered;
            destination.IsTaken = source.IsTaken;
            destination.Name = source.Name;
            destination.ParkingLotId = source.ParkingLotId;
            destination.Price = source.Price;
        }

        public static void CopyCity(City source, City destination)
        {
            destination.Name = source.Name;
        }

        public static void CopyArea(Area source, Area destination)
        {
            destination.Name = source.Name;
            destination.CityId = source.CityId;
        }
    }
}
