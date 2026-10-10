using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using Enhetsregistret.UI;

var repository = new EnhetsRepository();
var enhetsService = new EnhetsService(repository);

enhetsService.SeedData();

var UI = new ConsoleUI(enhetsService);
UI.Kor();
equipmentService.SeedData();
var UI = new ConsoleUI(equipmentService);
UI.Run();