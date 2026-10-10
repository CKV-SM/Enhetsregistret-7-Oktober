using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Models
{
    public class Cellphone : Equipment, IChargeable
    {
        public double RamGb { get; set; }
        public double ChargeLevel { get; set; }
        public Cellphone(int id, string name, decimal price, double ramGb) : base(id, name, price)
        {
            RamGb = ramGb;
        }

        public override string Description()
        {
            return $"{Name} – {Status} - RAM: {RamGb}gb";
        }

        public bool Charge()
        {
            Console.WriteLine("I am a phone and I am currently charging...");
            ChargeLevel = 100;
            return true;
        }
    }
}
