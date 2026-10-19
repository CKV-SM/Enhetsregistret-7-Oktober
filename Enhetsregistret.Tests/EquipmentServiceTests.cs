using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using Enhetsregistret.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Tests;

[TestClass]
public class EquipmentServiceTests
{
    IEquipmentService _sut;

    [TestMethod]
    public void ValidId_Returns_Equipment_When_Found()
    {
        //ARRANGE
        IEquipmentRepository fakeRepo = new FakeEquipmentRepository(
        new Cellphone(3, "Sony", 10000, 16)
        );

        IEquipmentService _sut = new EquipmentService(fakeRepo);

        // ACT
        Equipment? foundProduct = _sut.GetById(3);

        //ASSERT
        Assert.IsNotNull(foundProduct);
        Assert.AreEqual("Sony", foundProduct.Name);
        Assert.AreEqual(10000, foundProduct.Price);
    }
}
