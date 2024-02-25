using Microsoft.EntityFrameworkCore;
using ProductConsoleAPI.Business;
using ProductConsoleAPI.Business.Contracts;
using ProductConsoleAPI.Data.Models;
using ProductConsoleAPI.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace ProductConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestProductsDbContext dbContext;
        private IProductsManager productsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestProductsDbContext();
            this.productsManager = new ProductsManager(new ProductsRepository(this.dbContext));
        }
        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }
        private Product CreateNewValidProduct()
        {
            var random = new Random();
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = $"TestProduct{DateTime.Now.Ticks}",
                ProductCode = $"Test{DateTime.Now.Ticks}",
                Price = (decimal)(random.NextDouble() * (10 - 1) + 1),
                Quantity = random.Next(1, 1000),
                Description = "Test description"
            };
            return newProduct;
        }
        private Product CreateNewInvalidProduct()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "123",
                Price = -23.5m,
                Quantity = -1,
                Description = "Anything for description"
            };
            return newProduct;
        }
        //positive test
        [Test]
        public async Task AddProductAsync_ShouldAddNewProduct()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            // Act
            await productsManager.AddAsync(newProduct);
            var dbProduct = await this.dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);
            // Assert
            Assert.NotNull(dbProduct);
            Assert.AreEqual(newProduct, dbProduct);
        }
        //Negative test
        [Test]
        public async Task AddProductAsync_TryToAddProductWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            var invalidProduct = CreateNewInvalidProduct();
            // Act
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.AddAsync(invalidProduct));
            var actual = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductCode == invalidProduct.ProductCode);
            // Assert
            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid product!"));
        }
        [Test]
        public async Task DeleteProductAsync_WithValidProductCode_ShouldRemoveProductFromDb()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            await productsManager.DeleteAsync(newProduct.ProductCode);
            var product = await dbContext.Products.FirstOrDefaultAsync(s => s.ProductCode == newProduct.ProductCode);
            // Assert
            Assert.IsNull(product);
        }

        [Test]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task DeleteProductAsync_TryToDeleteWithNullOrWhiteSpaceProductCode_ShouldThrowException(string invalidProductCode)
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var ex = Assert.ThrowsAsync<ArgumentException>(() => productsManager.DeleteAsync(invalidProductCode));
            // Assert
            Assert.AreEqual("Product code cannot be empty.", ex.Message);
        }
        [Test]
        public async Task GetAllAsync_WhenProductsExist_ShouldReturnAllProducts()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            var newProduct1 = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            await productsManager.AddAsync(newProduct1);

            // Act
            var allProducts = await productsManager.GetAllAsync();

            // Assert
            Assert.AreEqual(allProducts.Count(), 2);
            Assert.AreEqual(allProducts.First(), newProduct);
            Assert.AreEqual(allProducts.Last(), newProduct1);
        }

        [Test]
        public async Task GetAllAsync_WhenNoProductsExist_ShouldThrowKeyNotFoundException()
        {
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async ()=> await productsManager.GetAllAsync());
            // Assert
            Assert.AreEqual("No product found.", ex.Message);
        }

        [Test]
        public async Task SearchByOriginCountry_WithExistingOriginCountry_ShouldReturnMatchingProducts()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var searchedProduct = await productsManager.SearchByOriginCountry(newProduct.OriginCountry);
            var dbProduct = await dbContext.Products.FirstOrDefaultAsync(s => s.OriginCountry == newProduct.OriginCountry);
            // Assert
            Assert.AreEqual(searchedProduct.ElementAt(0).OriginCountry, dbProduct.OriginCountry);
        }

        [Test]
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidOriginCountry = "Ghetto";
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.SearchByOriginCountry(invalidOriginCountry));
            // Assert
            Assert.AreEqual("No product found with the given first name.", ex.Message);
        }

        [Test]
        public async Task GetSpecificAsync_WithValidProductCode_ShouldReturnProduct()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var searchedProduct = await productsManager.GetSpecificAsync(newProduct.ProductCode);
            var product = await dbContext.Products.FirstOrDefaultAsync(s=> s.ProductCode == newProduct.ProductCode);
            // Assert
           Assert.AreEqual(searchedProduct, product);
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidProductCode_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidProductCode = "133533";
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetSpecificAsync(invalidProductCode));
            // Assert
            Assert.AreEqual($"No product found with product code: {invalidProductCode}", ex.Message);
        }

        [Test]
        public async Task UpdateAsync_WithValidProduct_ShouldUpdateProduct()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var updatedProduct = newProduct;
            updatedProduct.OriginCountry = "Bulgaristan";

            await productsManager.UpdateAsync(updatedProduct);
            var product = await dbContext.Products.FirstOrDefaultAsync(s => s.OriginCountry == updatedProduct.OriginCountry);
            // Assert
            Assert.AreEqual(product, updatedProduct);
        }

        [Test]
        public async Task UpdateAsync_WithInvalidProduct_ShouldThrowValidationException()
        {
            // Arrange
            var newProduct = CreateNewValidProduct();
            await productsManager.AddAsync(newProduct);
            // Act
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.UpdateAsync(null));
            // Assert
            Assert.AreEqual("Invalid prduct!", ex.Message);
        }
    }
}
