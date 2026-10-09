using Enhetsregistret.Models;

namespace Enhetsregistret.Services
{
    public interface IEnhetsService
    {
        Enhet[] GetAll();
        Enhet? GetById(int id);
        List<Enhet> SearchByName(string name);
        void Add(Enhet enhet);
        void SeedData();
    }
}