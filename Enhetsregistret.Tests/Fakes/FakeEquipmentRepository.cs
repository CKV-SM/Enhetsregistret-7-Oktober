using Enhetsregistret.Models;
using Enhetsregistret.Repositories;

namespace Enhetsregistret.Tests.Fakes
{
    public class FakeEquipmentRepository : IEquipmentRepository
    {
        private readonly List<Equipment> _equipment;

        public FakeEquipmentRepository(params Equipment[] initial)
        {
            _equipment = initial.ToList();
        }

        public void Add(Equipment equipment)
        {
            _equipment.Add(equipment);
        }

        public List<Equipment> GetAll()
        {
            return _equipment;
        }

        public void ReplaceAll(Equipment[] enhet)
        {
            _equipment.Clear();
            _equipment.AddRange(enhet);
        }

        public void Update(Equipment equipment)
        {
            int searchIndex = _equipment.FindIndex(eq => eq.Id == equipment.Id);
            if (searchIndex < 0)
                throw new InvalidOperationException("Equipment not found");

            _equipment[searchIndex] = equipment;
        }
    }
}
