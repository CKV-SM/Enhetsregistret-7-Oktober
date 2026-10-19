using Enhetsregistret.Services;

namespace Enhetsregistret.Tests;

[TestClass]
public class MathServiceTests
{
    IMathService _sut;

    [DataRow(4,8, 12)]
    [DataRow(2, 15, 17)]
    [DataRow(3, 10, 13)]
    [TestMethod]
    public void AddingTwo_Numbers_Should_Give_A_Valid_Sum(int firstNumber, int secondNumber, int result)
    {
        // ARRANGE
        _sut = new MathService();

        // ACT
        var sum = _sut.Add(firstNumber, secondNumber);

        Assert.AreEqual(result, sum);
    }

    [TestMethod]
    public void MultiplyingTwo_Numbers_Should_Give_A_Valid_Product()
    {
        // ARRANGE
        _sut = new MathService();
        int firstNumber = 3;
        int secondNumber = 8;

        // ACT
        var sum = _sut.Multiply(firstNumber, secondNumber);

        // ASSERT
        Assert.AreEqual(24, sum);
    }
}
