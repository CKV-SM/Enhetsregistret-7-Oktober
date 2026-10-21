using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Tests;

[TestClass]
public class LoanIntegrationServiceTests
{

    private string testDirectory = "";
    private string equipmentPath = string.Empty;
    private string loansPath = string.Empty;

    [TestInitialize]
    public void SetUp()
    {
        testDirectory = Path.Combine(
            Path.GetTempPath(),
            "EnhetsRegisterTests-" + Guid.NewGuid().ToString("N")
            );
        Directory.CreateDirectory(testDirectory);

        equipmentPath = Path.Combine(testDirectory, "equipment.json");
        loansPath = Path.Combine(testDirectory, "loans.json");

    }

    [TestCleanup]
    public void CleanUp()
    {

        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, recursive: true);
        }
    }

    [TestMethod]
    public void Loan_RegisteredEquipment_IsShownAfterReload() {

        // ARRANGE
        var equipmentService = new EquipmentService(
            new JsonEquipmentRepository(equipmentPath)
        );

        equipmentService.Add(new Cellphone(1, "Sony", 10000m, 8));

        var loanService = new LoanService(
            new JsonEquipmentRepository(equipmentPath),
            new JsonLoanRepository(loansPath)
        );

        // ACT
        loanService.Loan(1);

        var restartedApplication = new LoanService(
            new JsonEquipmentRepository(equipmentPath),
            new JsonLoanRepository(loansPath)
        );

        var result = restartedApplication.GetLoanedEquipment();

        // ASSERT
        Assert.HasCount(1, result);
        Assert.AreEqual("Sony", result[0].EquipmentName);

    }
}
