using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CedroRestauranteWS.Models;
using CedroRestauranteWS.Repositories;

namespace CedroRestauranteWS.Controllers
{
    [Produces("application/json")]
    [Route("api/Restaurantes")]
    public class RestaurantesController : ControllerBase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public RestaurantesController(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        // GET: api/Restaurantes
        [HttpGet]
        public IEnumerable<Restaurante> GetRestaurantes()
        {
            return _restauranteRepository.GetAll();
        }

        //GET: api/Restaurantes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurante([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurante = await _restauranteRepository.getWithPratos(id);
            if (restaurante == null)
                return NotFound();

            return Ok(restaurante);
        }

        //GET: api/Restaurantes?search="restaurante"
        [HttpGet("search")]
        public IActionResult FindRestaurantes([FromQuery] string nome)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (nome == null)
                return Ok(_restauranteRepository.GetAll());

            var restaurantes = _restauranteRepository.Find(r => r.Nome.ToLower().Contains(nome.ToLower()));
            return Ok(restaurantes);
        }

        // PUT: api/Restaurantes/5
        [HttpPut("{id}")]
        public IActionResult PutRestaurante([FromRoute] int id, [FromBody] Restaurante restaurante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != restaurante.Id)
                return BadRequest();

            try
            {
                _restauranteRepository.Update(restaurante);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestauranteExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Restaurantes
        [HttpPost]
        public IActionResult PostRestaurante([FromBody] Restaurante restaurante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            restaurante.Id = 0;
            _restauranteRepository.Add(restaurante);

            return CreatedAtAction("GetRestaurante", new { id = restaurante.Id }, restaurante);
        }

        // DELETE: api/Restaurantes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurante([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurante = _restauranteRepository.GetById(id);
            if (restaurante == null)
                return NotFound();

            _restauranteRepository.Delete(restaurante);
            return Ok(restaurante);
        }

        private bool RestauranteExists(int id)
        {
            return _restauranteRepository.GetById(id) != null ? true : false;
        }
    }
}