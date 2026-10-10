using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Enhetsregistret.Models
{
    public class Projector : Equipment
    {
        public int Lumen { get; set; }
        public Projector(int id, string name, decimal price, int lumen) : base(id, name, price)
        {
            Lumen = lumen;
        }

        public override string Description()
        {
            return $"{Name} – {Status} - Ljusstyrka: {Lumen} Lumen";
        }
    }
}
