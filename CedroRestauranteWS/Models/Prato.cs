using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Models
{
    public class Prato
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Preco { get; set; }

        public Restaurante Restaurante { get; set; }
    }
}
