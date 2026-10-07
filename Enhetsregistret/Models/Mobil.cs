using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Models
{
    public class Mobil : Enhet, ILaddningsbar
    {
        public double RamGb { get; set; }
        public double LaddningNiva { get; set; }

        public Mobil(int id, string namn, decimal pris, double ramGb) : base(id, namn, pris)
        {
            RamGb = ramGb;
        }

        public override string Beskrivning()
        {
            return $"{Namn} – {Status} - Arbetsminne (GB): {RamGb}gb";
        }

        public bool Ladda()
        {
            Console.WriteLine("Jag är en mobil och jag laddar just nu...");
            LaddningNiva = 100;
            return true;
        }
    }
}
