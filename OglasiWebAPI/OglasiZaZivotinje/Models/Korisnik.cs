namespace OglasiZaZivotinje.Models
{
    public class Korisnik
    {
        public int Sifra { get; set; }
        public int Uloga { get; set; }              
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Email { get; set; }

        public string Lozinka { get; set; }

        public string Mobitel { get; set; }

        public string Grad { get; set; }
    }
}
