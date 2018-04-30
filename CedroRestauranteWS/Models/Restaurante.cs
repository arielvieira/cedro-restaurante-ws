using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Models
{
    public class Restaurante
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public IList<Prato> Pratos { get; set; }
    }
}
