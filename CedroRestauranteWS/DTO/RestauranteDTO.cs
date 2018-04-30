using CedroRestauranteWS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CedroRestauranteWS.DTO
{
    public class RestauranteDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public RestauranteDTO(Restaurante restaurante)
        {
            Id = restaurante.Id;
            Nome = restaurante.Nome;
        }
    }
}