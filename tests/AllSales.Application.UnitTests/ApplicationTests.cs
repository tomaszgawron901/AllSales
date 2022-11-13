using AllSales.Application.Services;
using AllSales.Shared.Models;
using Moq;

namespace AllSales.Application.UnitTests;

[TestClass]
public class ApplicationTests
{
    [TestMethod]
    public async Task AddOrUpdate_is_called_for_every_item_retrieved_from_input_services_only_one_test()
    {
        // Arrange
        var product1 = CreateEmptyProduct();
        var product2 = CreateEmptyProduct();
        var product3 = CreateEmptyProduct();
        var product4 = CreateEmptyProduct();

        var inputServiceMock1 = new Mock<IInputService>();
        inputServiceMock1
            .Setup(x => x.GetSaleProducts())
            .ReturnsAsync(new List<Product> { product1, product2 });

        var inputServiceMock2 = new Mock<IInputService>();
        inputServiceMock2
            .Setup(x => x.GetSaleProducts())
            .ReturnsAsync(new List<Product> { product3, product4 });

        var outputServiceMock = new Mock<IOutputService>();

        var app = new Application(outputServiceMock.Object, inputServiceMock1.Object, inputServiceMock2.Object);

        // Act
        await app.Run();

        // Assert
        outputServiceMock.Verify(x => x.AddOrUpdate(product1), Times.Once);
        outputServiceMock.Verify(x => x.AddOrUpdate(product2), Times.Once);
        outputServiceMock.Verify(x => x.AddOrUpdate(product3), Times.Once);
        outputServiceMock.Verify(x => x.AddOrUpdate(product4), Times.Once);
    }

    private Product CreateEmptyProduct()
    {
        return new Product("", "", new Uri("https://netflix.com"), 0, 0, "", null);
    }
}