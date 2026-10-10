using Enhetsregistret.Models;

namespace Enhetsregistret.Repositories
{
    public interface IEquipmentRepository
    {
        List<Equipment> GetAll();
        void Add(Equipment equipment);
        void ReplaceAll(Equipment[] enhet);
    }
}