using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.DTO
{
    public class PratoInfoDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Preco { get; set; }

        public RestauranteDTO Restaurante { get; set; }
    }
}
