using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CedroRestauranteWS.Context;
using CedroRestauranteWS.Models;
using CedroRestauranteWS.DTO;

namespace CedroRestauranteWS.Controllers
{
    [Produces("application/json")]
    [Route("api/Pratos")]
    public class PratosController : Controller
    {
        private readonly RestauranteDbContext _context;

        public PratosController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: api/Pratos
        [HttpGet]
        public IEnumerable<PratoInfoDTO> GetPratos()
        {
            List<PratoInfoDTO> pratosDto = _context.Pratos.Include(p => p.Restaurante).Select(prato => new PratoInfoDTO()
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
            {
                return BadRequest(ModelState);
            }

            var prato = await _context.Pratos.Include(p => p.Restaurante).SingleOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }
            PratoInfoDTO pratoDto = new PratoInfoDTO()
            {
                Id = prato.Id,
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = new RestauranteDTO(prato.Restaurante)
            };
            if (prato == null)
            {
                return NotFound();
            }

            return Ok(pratoDto);
        }

        // PUT: api/Pratos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrato([FromRoute] int id, [FromBody] PratoDTO prato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != prato.Id)
                return BadRequest();

            var rest = await _context.Restaurantes.SingleOrDefaultAsync(m => m.Id == prato.RestauranteId);
            if (rest == null)
                return BadRequest();

            var newPrato = new Prato
            {
                Id = prato.Id,
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = rest
            };

            _context.Entry(newPrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> PostPrato([FromBody] PratoDTO prato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rest = await _context.Restaurantes.SingleOrDefaultAsync(m => m.Id == prato.RestauranteId);
            if (rest == null)
            {
                return BadRequest();
            }
            var newPrato = new Prato
            {
                Nome = prato.Nome,
                Preco = prato.Preco,
                Restaurante = rest
            };
            _context.Pratos.Add(newPrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrato", new { id = prato.Id }, newPrato);
        }

        // DELETE: api/Pratos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prato = await _context.Pratos.SingleOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            _context.Pratos.Remove(prato);
            await _context.SaveChangesAsync();

            return Ok(prato);
        }

        private bool PratoExists(int id)
        {
            return _context.Pratos.Any(e => e.Id == id);
        }
    }
}