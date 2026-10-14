using Enhetsregistret.DTO;
using Enhetsregistret.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Services
{
    public interface ILoanService
    {
        List<LoanDTO> GetLoanedEquipment();
        void Loan(int equipmentId);
    }
}
