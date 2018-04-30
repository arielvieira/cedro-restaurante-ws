using CedroRestauranteWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Repositories
{
    public interface IRestauranteRepository : IRepository<Restaurante>
    {
        Task<Restaurante> getWithPratos(int id);
    }
}
