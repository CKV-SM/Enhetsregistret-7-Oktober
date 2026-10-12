using Enhetsregistret.Models;

namespace Enhetsregistret.Services
{
    public interface IEquipmentService
    {
        Equipment[] GetAll();
        Equipment? GetById(int id);
        List<Equipment> SearchByName(string name);
        void Add(Equipment equipment);
        void SeedData();
        void UpdateItem(Equipment newState);
    }
}