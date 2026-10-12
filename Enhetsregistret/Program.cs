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

var jsonPath = Path.Combine(dataFolder, "equipment.json");

var jsonRepository = new JsonEquipmentRepository(jsonPath);
//var memoryRepository = new EquipmentRepository();

var enhetsService = new EquipmentService(jsonRepository);
enhetsService.SeedData();


var UI = new ConsoleUI(enhetsService);
UI.Run();
