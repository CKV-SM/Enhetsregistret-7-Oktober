using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
