using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using Enhetsregistret.UI;

var repository = new EquipmentRepository();
var enhetsService = new EquipmentService(repository);

enhetsService.SeedData();

var UI = new ConsoleUI(enhetsService);
UI.Run();