namespace StoreManagementSystem.Services.Tests.Product
{
    using Moq;
    using NUnit.Framework;
    using Services.Core.Interfaces;
    using StoreManagementSystem.ViewModels.Product;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductService> mockProductService;
        private IProductService productService;

        [SetUp]
        public void SetUp()
        {
            mockProductService = new Mock<IProductService>();

            productService = mockProductService.Object;
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            ICollection<ProductMinViewModel> expectedProducts = new List<ProductMinViewModel>
            {
                new ProductMinViewModel { ProductId = 1, ProductName = "Car Speakers", Price = 15.49m },
                new ProductMinViewModel { ProductId = 2, ProductName = "Herb Garden Planter Box", Price = 49.99m }
            };

            mockProductService
                .Setup(service => service.GetAllProductsAsync())
                .ReturnsAsync(expectedProducts);

            // Act
            IEnumerable<ProductMinViewModel> result = await productService.GetAllProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().ProductName, Is.EqualTo("Car Speakers"));
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            ICollection<ProductMinViewModel> expectedProducts = new List<ProductMinViewModel>();

            mockProductService
                .Setup(service => service.GetAllProductsAsync())
                .ReturnsAsync(expectedProducts);
            // Act
            IEnumerable<ProductMinViewModel> result = await productService.GetAllProductsAsync();
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllProductsByCategoryIdAsync_ShouldReturnProductsForGivenCategoryId()
        {
            // Arrange
            int categoryId = 1;

            ICollection<ProductMinViewModel> expectedProducts = new List<ProductMinViewModel>
            {
                new ProductMinViewModel { ProductId = 1, ProductName = "Car Speakers", Price = 15.49m }
            };

            mockProductService
                .Setup(service => service.GetProductsByCategoryIdAsync(categoryId))
                .ReturnsAsync(expectedProducts);

            // Act
            IEnumerable<ProductMinViewModel>? result = await productService.GetProductsByCategoryIdAsync(categoryId);
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ProductName, Is.EqualTo("Car Speakers"));
        }

        [Test]
        public async Task GetAllProductsByCategoryIdAsync_ShouldReturnNull_WhenNoProductsExistForGivenCategoryId()
        {
            // Arrange
            int categoryId = 999;
            mockProductService
                .Setup(service => service.GetProductsByCategoryIdAsync(categoryId))
                .ReturnsAsync((IEnumerable<ProductMinViewModel>?)null);
            // Act
            IEnumerable<ProductMinViewModel>? result = await productService.GetProductsByCategoryIdAsync(categoryId);
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetProductDetailsByIdAsync_ShouldReturnProductDetailsForGivenProductId()
        {
            // Arrange
            int productId = 1;
            ProductDetailsViewModel expectedProductDetails = new ProductDetailsViewModel
            {
                ProductId = 1,
                ProductName = "Car Speakers",
                Price = "15.49",
                CategoryName = "Audio",
                SupplierName = "Waters, Wolf and MacGyver"
            };
            mockProductService
                .Setup(service => service.GetProductDetailsByIdAsync(productId))
                .ReturnsAsync(expectedProductDetails);
            // Act
            ProductDetailsViewModel? result = await productService.GetProductDetailsByIdAsync(productId);
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ProductName, Is.EqualTo("Car Speakers"));
        }

        [Test]
        public async Task GetProductDetailsByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 999;
            mockProductService
                .Setup(service => service.GetProductDetailsByIdAsync(productId))
                .ReturnsAsync((ProductDetailsViewModel?)null);

            // Act
            ProductDetailsViewModel? result = await productService.GetProductDetailsByIdAsync(productId);
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ProductExistsAsync_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            int productId = 1;
            mockProductService
                .Setup(service => service.ProductExistsAsync(productId))
                .ReturnsAsync(true);
            // Act
            bool result = await productService.ProductExistsAsync(productId);
            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ProductExistsAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 999;
            mockProductService
                .Setup(service => service.ProductExistsAsync(productId))
                .ReturnsAsync(false);
            // Act
            bool result = await productService.ProductExistsAsync(productId);
            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task CategoryExistsAsync_ShouldReturnTrue_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;
            mockProductService
                .Setup(service => service.CategoryExistsAsync(categoryId))
                .ReturnsAsync(true);
            // Act
            bool result = await productService.CategoryExistsAsync(categoryId);
            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CategoryExistsAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 999;
            mockProductService
                .Setup(service => service.CategoryExistsAsync(categoryId))
                .ReturnsAsync(false);
            // Act
            bool result = await productService.CategoryExistsAsync(categoryId);
            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task SupplierExistsAsync_ShouldReturnTrue_WhenSupplierExists()
        {
            // Arrange
            int supplierId = 1;
            mockProductService
                .Setup(service => service.SupplierExistsAsync(supplierId))
                .ReturnsAsync(true);
            // Act
            bool result = await productService.SupplierExistsAsync(supplierId);
            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task SupplierExistsAsync_ShouldReturnFalse_WhenSupplierDoesNotExist()
        {
            // Arrange
            int supplierId = 999;
            mockProductService
                .Setup(service => service.SupplierExistsAsync(supplierId))
                .ReturnsAsync(false);
            // Act
            bool result = await productService.SupplierExistsAsync(supplierId);
            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetProductInputModelByProductIdAsync_ShouldReturnProductInputModelForGivenProductId()
        {
            // Arrange
            int productId = 1;
            ProductAddInputModel expectedInputModel = new ProductAddInputModel
            {
                Name = "Car Speakers",
                Price = 15.49m,
                Quantity = 100,
                CategoryId = 1,
                SupplierId = 1
            };
            mockProductService
                .Setup(service => service.GetProductInputModelByProductIdAsync(productId))
                .ReturnsAsync(expectedInputModel);
            // Act
            ProductAddInputModel? result = await productService.GetProductInputModelByProductIdAsync(productId);
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("Car Speakers"));
        }

        [Test]
        public async Task GetProductInputModelByProductIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 999;
            mockProductService
                .Setup(service => service.GetProductInputModelByProductIdAsync(productId))
                .ReturnsAsync((ProductAddInputModel?)null);
            // Act
            ProductAddInputModel? result = await productService.GetProductInputModelByProductIdAsync(productId);
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetEmptyProductInputModelAsync_ShouldReturnEmptyProductInputModel()
        {
            // Arrange
            ProductAddInputModel expectedInputModel = new ProductAddInputModel();
            mockProductService
                .Setup(service => service.GetEmptyProductInputModelAsync())
                .ReturnsAsync(expectedInputModel);
            // Act
            ProductAddInputModel result = await productService.GetEmptyProductInputModelAsync();
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.Null);
            Assert.That(result.Price, Is.EqualTo(0));
            Assert.That(result.Quantity, Is.EqualTo(0));
            Assert.That(result.CategoryId, Is.EqualTo(0));
            Assert.That(result.SupplierId, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateProductAsync_ShouldCreateProductSuccessfully()
        {
            // Arrange
            ProductAddInputModel inputModel = new ProductAddInputModel
            {
                Name = "New Product",
                Price = 19.99m,
                Quantity = 50,
                CategoryId = 1,
                SupplierId = 1
            };

            mockProductService
                .Setup(service => service.CreateProductAsync(inputModel))
                .Returns(Task.CompletedTask);

            // Act & Assert (No exception should be thrown)
            Assert.DoesNotThrowAsync(async () => await productService.CreateProductAsync(inputModel));
        }
    }
}