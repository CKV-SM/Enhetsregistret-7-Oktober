using Enhetsregistret.DTO;
using Enhetsregistret.Models;
using Enhetsregistret.Repositories;

namespace Enhetsregistret.Services;

public class LoanService : ILoanService
{
    private readonly IEquipmentRepository equipmentRepo;
    private readonly ILoanRepository loanRepo;

    public LoanService(
        IEquipmentRepository equipmentRepo,
        ILoanRepository loanRepo
    )
    {
        this.equipmentRepo = equipmentRepo;
        this.loanRepo = loanRepo;
    }



    public List<LoanDTO> GetLoanedEquipment()
    {
        var allEquipment = equipmentRepo.GetAll();
        var allLoans = loanRepo.GetAll();
            
        return allLoans
            .Where(loan => loan.ReturnDate is null)
            .Select(loan => new LoanDTO {
                EquipmentName = allEquipment.First(eq => eq.Id == loan.EquipmentId).Name,
                BorrowDate = loan.StartDate,
                ReturnDate = loan.ReturnDate
            }).ToList();
    }

    public void Loan(int equipmentId)
    {
        var allEquipment = equipmentRepo.GetAll();
        var allLoans = loanRepo.GetAll();

        var searchItem = allEquipment.FirstOrDefault(eq => eq.Id == equipmentId);
        var itemExistsInRegistry = searchItem is not null;

        if (!itemExistsInRegistry) {
            throw new Exception("Utrustning fanns inte i systemet");
        }

        var isCurrentlyBorrowed = allLoans
            .Any(loan => loan.EquipmentId == equipmentId && loan.ReturnDate is null);

        if (isCurrentlyBorrowed)
        {
            throw new Exception("Du försöker låna en utrustning som redan är utlånad.");
        }

        var newLoanId = 1;
        if (allLoans.Count > 0)
        {
            newLoanId = allLoans.Max(loan => loan.Id) + 1;
        }

        try
        {
            var loanToCreate = new Loan
            {
                Id = newLoanId,
                EquipmentId = searchItem!.Id,
                StartDate = DateTime.UtcNow
            };

            loanRepo.Add(loanToCreate);
        } catch(Exception e)
        {
            throw new Exception("Lån gick inte att utföra. Anledning: ", e);
        }
    }
}
