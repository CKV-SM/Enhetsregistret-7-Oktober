// File.WriteAllText("vart man vill skriva / vilken fil", "vilken text du vill spara");
//string readValue = File.ReadAllText("filepath");

using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using Enhetsregistret.UI;
using System.Text.Json;

string basePath = AppContext.BaseDirectory;
string dataFolder = Path.Combine(basePath, "data");


if (!Directory.Exists(dataFolder))
    Directory.CreateDirectory(dataFolder);

var jsonEquipmentPath = Path.Combine(dataFolder, "equipment.json");
var jsonLoanPath = Path.Combine(dataFolder, "loans.json");

var jsonEquipmentRepository = new JsonEquipmentRepository(jsonEquipmentPath);
var jsonLoanRepository = new JsonLoanRepository(jsonLoanPath);
var equipmentService = new EquipmentService(jsonEquipmentRepository);

var loanService = new LoanService(
    jsonEquipmentRepository, 
    jsonLoanRepository
);

equipmentService.SeedData();

var loanMenu = new LoanMenu(loanService);
var equipmentMenu = new EquipmentMenu(equipmentService);

var UI = new MainMenu(equipmentMenu, loanMenu);

UI.Run();