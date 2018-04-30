using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CedroRestauranteWS.Models;
using CedroRestauranteWS.DTO;
using CedroRestauranteWS.Repositories;

namespace CedroRestauranteWS.Controllers
{
    [Produces("application/json")]
    [Route("api/Pratos")]
    public class PratosController : ControllerBase
    {
        private readonly IPratoRepository _pratoRepository;
        private readonly IRestauranteRepository _restauranteRepository;

        public PratosController(IPratoRepository pratoRepository, IRestauranteRepository restauranteRepository)
        {
            _pratoRepository = pratoRepository;
            _restauranteRepository = restauranteRepository;
        }

        // GET: api/Pratos
        [HttpGet]
        public IEnumerable<PratoInfoDTO> GetPratos()
        {
            List<PratoInfoDTO> pratosDto = _pratoRepository.GetAllWithRestaurante().Select(prato => new PratoInfoDTO()
            {
                Id = prato.Id,
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = new RestauranteDTO(prato.Restaurante)
            }).ToList();
            return pratosDto;
        }

        // GET: api/Pratos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prato = await _pratoRepository.GetWithRestaurante(id);
            if (prato == null)
                return NotFound();

            PratoInfoDTO pratoDto = new PratoInfoDTO()
            {
                Id = prato.Id,
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = new RestauranteDTO(prato.Restaurante)
            };

            return Ok(pratoDto);
        }

        //PUT: api/Pratos/5
        [HttpPut("{id}")]
        public IActionResult PutPrato([FromRoute] int id, [FromBody] PratoDTO prato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != prato.Id)
                return BadRequest();

            var restaurante = _restauranteRepository.GetById(prato.RestauranteId);
            if (restaurante == null)
                return BadRequest();

            var newPrato = new Prato
            {
                Id = prato.Id,
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = restaurante
            };

            try
            {
                _pratoRepository.Update(newPrato);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PratoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Pratos
        [HttpPost]
        public IActionResult PostPrato([FromBody] PratoDTO prato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurante = _restauranteRepository.GetById(prato.RestauranteId);
            if (restaurante == null)
                return BadRequest();

            var newPrato = new Prato
            {
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = restaurante
            };
            _pratoRepository.Add(newPrato);
            return CreatedAtAction("GetPrato", new { id = prato.Id }, newPrato);
        }

        // DELETE: api/Pratos/5
        [HttpDelete("{id}")]
        public IActionResult DeletePrato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prato = _pratoRepository.GetById(id);
            if (prato == null)
                return NotFound();

            _pratoRepository.Delete(prato);
            return Ok(prato);
        }

        private bool PratoExists(int id)
        {
            return _pratoRepository.GetById(id) != null ? true : false;
        }
    }
}