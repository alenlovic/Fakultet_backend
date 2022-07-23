using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjekatFakultet.DataBase;
using ProjekatFakultet.Models;

namespace ProjekatFakultet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.Admin)]
    public class ProfesoriController : ControllerBase
    {
        private ILogiranje _logiranje;
        private readonly ApplicationDbContext _context;

        public ProfesoriController(ILogiranje logiranje, ApplicationDbContext context)
        {
            _logiranje = logiranje;
            _context = context;
        }

        // GET: api/Profesori
        [HttpGet]
        public List<ProfesoriModel> GetProfesoriModel()
        {
            var listaProfesora = _context.Profesori.ToList();
            return listaProfesora;
        }

        // QUERY LINQ 
        // GET api/profesori/plate
        [HttpGet]
        [Route("Plate")]
        public List<ProfesoriModel> GetProfesoriPlate()
        {
            var lista = from profesor in _context.Profesori
                        where profesor.Plata >= 1900
                        select profesor;

            return lista.ToList();
        }

        // GET: api/Profesori/Prezime
        [HttpGet]
        [Route("Prezime")]
        public IActionResult GetPrezime()
        {

            var lista = _context.Profesori.Where(p => p.Prezime.EndsWith("ić")).ToList();

            return Ok(lista);
        }


        // GET: api/Profesori/godiste
        [HttpGet]
        [Route("Godiste")]
        public IActionResult GetGodisteProfesora()
        {
            var lista = _context.Profesori.Where(p => p.Godiste.Equals(1971));

            return Ok(lista);
        }

        // PUT: api/Profesori/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesoriModel(int id, ProfesoriModel profesoriModel)
        {
            if (id != profesoriModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(profesoriModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoriModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Profesori
        [HttpPost]
        public async Task<ActionResult<ProfesoriModel>> PostProfesoriModel(ProfesoriModel profesoriModel)
        {
            _context.Profesori.Add(profesoriModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesoriModel", new { id = profesoriModel.ID }, profesoriModel);
        }

        // DELETE: api/Profesori/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesoriModel(int id)
        {
            var profesoriModel = await _context.Profesori.FindAsync(id);
            if (profesoriModel == null)
            {
                return NotFound();
            }

            _context.Profesori.Remove(profesoriModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesoriModelExists(int id)
        {
            return _context.Profesori.Any(e => e.ID == id);
        }
    }
}
