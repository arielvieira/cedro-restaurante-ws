using CedroRestauranteWS.Context;
using CedroRestauranteWS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Repositories
{
    public class RestauranteRepository : Repository<Restaurante>, IRestauranteRepository
    {
        public RestauranteRepository(RestauranteDbContext context) : base(context)
        {
        }

        public async Task<Restaurante> getWithPratos(int id)
        {
            return await _context.Restaurantes.Include(p => p.Pratos).SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
