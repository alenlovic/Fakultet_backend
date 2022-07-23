using System.Diagnostics;

namespace ProjekatFakultet.Models
{
    public class LogiranjeUSQLBazuPodataka : ILogiranje
    {
        public void Info()
        {
            Debug.WriteLine("Uspješno upisivanje log-a!");
        }
    }
}
