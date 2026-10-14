using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.DTO;

public class LoanDTO
{
    public string EquipmentName { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
