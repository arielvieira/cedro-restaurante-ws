using CedroRestauranteWS.Context;
using CedroRestauranteWS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Repositories
{
    public class PratoRepository : Repository<Prato>, IPratoRepository
    {
        public PratoRepository(RestauranteDbContext context) : base(context)
        {
        }

        public async Task<Prato> GetWithRestaurante(int id)
        {
            return await _context.Pratos.Include(p => p.Restaurante).SingleOrDefaultAsync(m => m.Id == id);
        }

        public IEnumerable<Prato> GetAllWithRestaurante()
        {
            return _context.Pratos.Include(p => p.Restaurante);
        }
    }
}
