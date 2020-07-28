using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        [MaxLength(12)]
        public string UserName { get; set; }
        [MaxLength(12)]
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string PhoneNumber { get; set; }

        //Navigation Properties
        public ICollection<ParkingLot> ParkingLots { get; set; }
        
        protected string CombineNames()
        {
            return $"{FirstName} {LastName}";
        }
        

    }
}
