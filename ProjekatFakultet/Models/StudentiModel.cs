using System.ComponentModel.DataAnnotations;

namespace ProjekatFakultet.Models
{
    public class StudentiModel
    {
        [Key]
        public int ID { get; set; } 
        public string Ime { get; set; }
        public string Prezime { get; set; } 
        public int Godiste { get; set; }
        public int BrojIndeksa { get; set; }
        public int GodinaStudija { get; set; }  
        public string Smjer { get; set; }
        public double ProsjekOcjena { get; set; }
        public bool RedovnaNastava { get; set; }
    }
}
