using System.ComponentModel.DataAnnotations;

namespace ProjekatFakultet.Models
{
    public class UpravaModel
    {
        [Key]
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pozicija { get; set; }
    }
}
