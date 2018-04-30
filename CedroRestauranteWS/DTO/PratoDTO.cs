using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.DTO
{
    public class PratoDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Range(0, double.MaxValue)]
        public double Preco { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public int RestauranteId { get; set; }
    }
}
