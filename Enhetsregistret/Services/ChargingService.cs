using Enhetsregistret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Services
{
    public class ChargingService
    {
        public void Charge(IChargeable enhet) {
            bool chargingSuccessful = enhet.Charge();
            if (chargingSuccessful)
            {
                Console.WriteLine("Laddning gick bra");
            }
            else {
                Console.WriteLine("Laddning gick inte bra :(");
            }
        }
    }
}
