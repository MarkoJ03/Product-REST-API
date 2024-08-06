using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service;
using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class ProductServiceTests
{
    private readonly IProductRepository _mockProductRepository;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockProductRepository = Substitute.For<IProductRepository>();
        _productService = new ProductService(_mockProductRepository);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = (float)10.0M , Description="Product1"},
            new Product { Id = 2, Name = "Product2", Price = (float)20.0M , Description="Product1"}
        };
        _mockProductRepository.GetProducts().Returns(products);

        // Act
        var result = await _productService.GetAllProducts();

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Product1");
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1", Price = (float)10.0M, Description = "Product1" };
        _mockProductRepository.GetProductByID(1).Returns(product);

        // Act
        var result = await _productService.GetProductById(1);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Product1");
    }

    [Fact]
    public async Task AddProduct_ShouldReturnNewProduct()
    {
        // Arrange
        var productDto = new ProductDTO {Id=1, Name = "Product1", Price = (float)10.0M, Description = "Product1" };
        var product = new Product { Id = 1, Name = "Product1", Price = (float)10.0M, Description = "Product1" };
        _mockProductRepository.InsertProduct(Arg.Any<Product>()).Returns(product);

        // Act
        var result = await _productService.AddProduct(productDto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Product1");
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnUpdatedProduct()
    {
        // Arrange
        var productDto = new ProductDTO { Id = 1, Name = "UpdatedProduct", Price = (float)15.0M, Description = "Product1" };
        var product = new Product { Id = 1, Name = "UpdatedProduct", Price = (float)15.0M, Description = "Product1" };
        _mockProductRepository.UpdateProduct(Arg.Any<Product>()).Returns(product);

        // Act
        var result = await _productService.UpdateProduct(productDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("UpdatedProduct");
    }

    [Fact]
    public async Task DeleteProduct_ShouldCallRepository()
    {
        // Arrange
        var productId = 1;

        // Act
        await _productService.DeleteProduct(productId);

        // Assert
        await _mockProductRepository.Received(1).DeleteProduct(productId);
    }
}
