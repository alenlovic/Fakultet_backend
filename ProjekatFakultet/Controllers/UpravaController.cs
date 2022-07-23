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
    [Authorize(Policy = Policies.Service)]
    public class UpravaController : ControllerBase
    {

        private ILogiranje _logiranje;
        private readonly ApplicationDbContext _context;

        public UpravaController(ILogiranje logiranje, ApplicationDbContext context)
        {
            _context = context;
            _logiranje = logiranje;
        }

        // GET: api/Uprava
        [HttpGet]
        public List<UpravaModel> GetUprava()
        {
            var listaUprave = _context.Uprava.ToList();
            return listaUprave;
        }

        // GET: api/Uprava/Dekanat
        [HttpGet("Dekanat")]
        public IActionResult GetUpravaModel()
        {
            var dekanat = _context.Uprava.Where(u => u.Pozicija == "Dekan");

            return Ok(dekanat);
        }

        // PUT: api/Uprava/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpravaModel(int id, UpravaModel upravaModel)
        {
            if (id != upravaModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(upravaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpravaModelExists(id))
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

        // POST: api/Uprava
        [HttpPost]
        public async Task<ActionResult<UpravaModel>> PostUpravaModel(UpravaModel upravaModel)
        {
            _context.Uprava.Add(upravaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUpravaModel", new { id = upravaModel.ID }, upravaModel);
        }

        // DELETE: api/Uprava/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUpravaModel(int id)
        {
            var upravaModel = await _context.Uprava.FindAsync(id);
            if (upravaModel == null)
            {
                return NotFound();
            }

            _context.Uprava.Remove(upravaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UpravaModelExists(int id)
        {
            return _context.Uprava.Any(e => e.ID == id);
        }
    }
}
