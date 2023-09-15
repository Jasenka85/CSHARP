namespace EdunovaApp.Models
{
    public class Polaznik : Entitet
    {
        public string? Ime { get; set; }
        public string? Prezime { get; set; }

        public string? Oib { get; set; }

        public string? Email { get; set; }

        public ICollection<Grupa> Grupe { get; } = new List<Grupa>();
        //s lijeve strane sučelje, s desne implementacijska klasa


    }
}
