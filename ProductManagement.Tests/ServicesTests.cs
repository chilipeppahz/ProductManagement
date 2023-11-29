using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Entities;

namespace ProductManagement.Tests;
public class ServicesTests
{
    [Fact]
    public void GetProductById_ReturnsNotFound_ForNonexistentProduct()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "GetProductById_ReturnsNotFound")
            .Options;

        using (var context = new AppDbContext(options))
        {
            var controller = new AppServices(context);

            // Act
            var result = controller.GetProductById(1);

            // Assert
            Assert.IsType<ActionResult<Product>>(result);
            var actionResult = result;
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }

    [Fact]
    public void GetProductById_ReturnsProduct_ForExistingProduct()
    {
        // Arrange
        var existingProduct = new Product { Id = 1, Name = "Test Product" };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "GetProductById_ReturnsProduct")
            .Options;

        using (var context = new AppDbContext(options))
        {
            context.Products.Add(existingProduct);
            context.SaveChanges();

            var controller = new AppServices(context);

            // Act
            var result = controller.GetProductById(1);

            // Assert
            Assert.IsType<ActionResult<Product>>(result);
            var actionResult = result;
            Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(existingProduct, actionResult.Value);
        }
    }

    [Fact]
    public void CreateProduct_ReturnsCreatedAtAction()
    {
        // Arrange
        var newProduct = new Product { Name = "New Product" };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateProduct_ReturnsCreatedAtAction")
            .Options;

        using (var context = new AppDbContext(options))
        {
            var controller = new AppServices(context); 

            // Act
            var result = controller.CreateProduct(newProduct);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }

}
