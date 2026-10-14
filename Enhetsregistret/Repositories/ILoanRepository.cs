using Enhetsregistret.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Repositories;
public interface ILoanRepository
{
    List<Loan> GetAll();
    void Add(Loan loan);
    void ReplaceAll(Loan[] loan);
    void Update(Loan loan);
}
