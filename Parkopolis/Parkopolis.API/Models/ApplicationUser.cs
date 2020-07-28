using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(12)]
        public string LastName { get; set; }
        public UserType Type { get; set; }
        //Navigation Properties
        //public ICollection<ParkingLot> ParkingLots { get; set; }
        
        //protected string CombineNames()
        //{
        //    return $"{FirstName} {LastName}";
        //}
        

    }
}
