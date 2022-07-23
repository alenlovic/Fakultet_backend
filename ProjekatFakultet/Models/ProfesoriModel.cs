using System.ComponentModel.DataAnnotations;

namespace ProjekatFakultet.Models
{
    public class ProfesoriModel
    {
        [Key]
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godiste { get; set; }
        public string Predmet { get; set; }
        public int Plata { get; set; }
    }
}
