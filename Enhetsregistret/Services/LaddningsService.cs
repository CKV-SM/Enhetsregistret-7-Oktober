using Enhetsregistret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Services
{
    public class LaddningsService
    {
        public void Ladda(ILaddningsbar enhet) {
            bool gickAttLadda = enhet.Ladda();
            if (gickAttLadda)
            {
                Console.WriteLine("Laddning gick bra");
            }
            else {
                Console.WriteLine("Laddning gick inte bra :(");
            }
        }
    }
}
