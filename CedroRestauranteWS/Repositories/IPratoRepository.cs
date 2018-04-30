using CedroRestauranteWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Repositories
{
    public interface IPratoRepository : IRepository<Prato>
    {
        Task<Prato> GetWithRestaurante(int id);
        IEnumerable<Prato> GetAllWithRestaurante();
    }
}
