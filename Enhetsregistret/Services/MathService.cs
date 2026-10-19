namespace Enhetsregistret.Services;

public class MathService : IMathService
{
    public int Add(int no1, int no2)
    {
        return no1 + no2;
    }

    public double Multiply(int no1, int no2)
    {
        return no1 * no2;
    }
}
