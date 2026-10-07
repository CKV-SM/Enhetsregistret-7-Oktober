using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetsregistret.Models
{
    public interface ILaddningsbar
    {
        double LaddningNiva { get; set; }
        bool Ladda();
    }
}
