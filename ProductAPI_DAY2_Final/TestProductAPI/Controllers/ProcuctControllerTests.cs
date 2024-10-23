using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Services;

namespace TestProductAPI.Controllers
{

    public class ProcuctControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _productController;
        public ProcuctControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _productController = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task CreateProduct_ReturnCreateAtAction_whenProductCreated()
        {
            // Arrange
            int productId = 1;
            var newProduct = new Product
            {
                Name = "Product A",
                Price = 100
            };
            _mockProductService
                .Setup(s => s.CreateProduct(newProduct))
                .ReturnsAsync(productId);

            // Act
            var result = await _productController.CreateProduct(newProduct);


            // Assert
            var createdActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(productId, createdActionResult.RouteValues["id"]);

            var returnProduct = Assert.IsType<Product>(createdActionResult.Value);
            Assert.Equal(newProduct.Name, returnProduct.Name);
            Assert.Equal(newProduct.Price, returnProduct.Price);
        }

        [Fact]
        public async Task DeleteProduct_RetrunNoContent_WhenProducyIsDeleted()
        {
            // Arrange
            int producyId = 1;
            _mockProductService
             .Setup(s => s.DeleteProduct(producyId))
             .ReturnsAsync(true);


            // Act
            var result = await _productController.DeleteProduct(producyId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        //TODO DeleteProduct...Testcase NotFound();


        [Fact]
        public async Task GetProduct_ReturnOkresult_withListOfProducts()
        {
            // Arrange
            var listProduct = new List<Product>
            { 
                new Product{ Id=1, Name ="ProductA", Price =10},
                new Product{ Id=2, Name ="Product B", Price =20}
            };

            _mockProductService
                .Setup(s => s.GetProducts())
                .ReturnsAsync(listProduct);

            // Act
            var result = await _productController.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnProduct = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Equal(2, ((List<Product>)returnProduct).Count());
        }
               

        [Fact]
        public async Task GetProductById_ReturnResult_WithProduct_WhenExists()
        {
            // Arrange
            int productId = 1;
            var product = new Product { Id = 1, Name = "ProductA", Price = 10 };

            _mockProductService
                .Setup(s => s.GetProductById(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _productController.GetProduct(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(productId, returnProduct.Id);
        }

        //TODO GetProduct...Testcase NotFound();

        [Fact]
        public async Task UpdateProduct_ReturnNoContent_WhenProductIsUpdate()
        {
            // Arrange
            int productId = 1;
            var productUpdate = new Product
            {
                Id = 1,
                Name = "Product A",
                Price = 10
            };
            _mockProductService
              .Setup(s => s.UpdateProduct(productUpdate))
              .ReturnsAsync(true);


            // Act
            var result = await _productController.UpdateProduct(productId, productUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        //TODO UpdateProduct...Testcase BadRequest();

        //TODO UpdateProduct...Testcase NotFound();
    }
}
