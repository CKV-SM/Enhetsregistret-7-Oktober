using Enhetsregistret.Models;

namespace Enhetsregistret.Repositories
{
    public interface IEnhetsRepository
    {
        List<Enhet> HamtaAlla();
        void LaggTill(Enhet enhet);
        void LaggTillAlla(Enhet[] enhet);
    }
}