using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatFakultet.DataBase;
using ProjekatFakultet.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjekatFakultet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.User)]
    public class StudentiController : ControllerBase
    {
        private ILogiranje _logiranje;
        private ApplicationDbContext _context;

        public StudentiController(ILogiranje logiranje, ApplicationDbContext context)
        {
            _logiranje = logiranje;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logiranje.Info();

            var listaStudenata = new List<StudentiModel>();

            var student1 = new StudentiModel { Ime = "Alen", Prezime = "Lović", Smjer = "Računarstvo", Godiste = 1999, BrojIndeksa = 184, RedovnaNastava = false};

            var student2 = new StudentiModel { Ime = "Dino", Prezime = "Čeljo", Smjer = "Grafički dizajn", Godiste = 1997, BrojIndeksa = 182, RedovnaNastava = true };

            listaStudenata.Add(student1);
            listaStudenata.Add(student2);

            try
            {
                return Ok(listaStudenata);
            }
            catch
            {
                var greska = "greska";
                return Problem(detail: greska);
            }
        }

        [HttpGet]
        [Route("prosjekocjena/{prosjek}")]
        public IActionResult GetProsjekOcjena(int prosjek)
        {
            var lista = _context.Studenti.Where(s => s.ProsjekOcjena >= prosjek).ToList();


            return Ok(lista);
        }

        [HttpGet]
        [Route("prezime/{prezime}")]
        public IActionResult GetPrezime(string prezime)
        {
            var lista = _context.Studenti.Where(s => s.Prezime.StartsWith("L"));

            return Ok(lista);
        }

        [HttpGet]
        [Route("Average")]
        public IActionResult GetUkupniProsjek()
        {
            var ukupniProsjek = _context.Studenti.Average(s => s.ProsjekOcjena);

            return Ok(ukupniProsjek);
        }

        [HttpPost]
        public IActionResult Post(StudentiModel studenti)
        {
            _context.Studenti.Add(studenti);

            _context.SaveChanges();

            return Ok();
        }

    }
}
