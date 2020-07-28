using Parkopolis.API.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Services
{
    public class ParkopolisDbRepository : IParkopolisRepository
    {
        private readonly ParkopolisDbContext _context;

        public ParkopolisDbRepository(ParkopolisDbContext context)
        {
            _context = context;
        }
    }
}
