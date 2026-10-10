using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Models
{
    public interface IChargeable
    {
        double ChargeLevel { get; set; }
        bool Charge();
    }
}
