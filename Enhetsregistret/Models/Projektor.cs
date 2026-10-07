using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Models
{
    public class Projektor : Enhet
    {
        public int Lumen { get; set; }
        public Projektor(int id, string namn, decimal pris, int ljusstyrka) : base(id, namn, pris)
        {
            Lumen = ljusstyrka;
        }

        public override string Beskrivning()
        {
            return $"{Namn} – {Status} - Ljusstyrka: {Lumen} Lumen";
        }
    }
}
