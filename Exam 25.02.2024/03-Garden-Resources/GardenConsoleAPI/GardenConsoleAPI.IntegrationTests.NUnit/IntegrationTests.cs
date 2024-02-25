using GardenConsoleAPI.Business;
using GardenConsoleAPI.Business.Contracts;
using GardenConsoleAPI.Data.Models;
using GardenConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace GardenConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestPlantsDbContext dbContext;
        private IPlantsManager plantsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestPlantsDbContext();
            this.plantsManager = new PlantsManager(new PlantsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }
        private Plant CreateNewValidPlant()
        {
            var random = new Random();
            var newPlant = new Plant()
            {
                Id = (int)DateTime.Now.Ticks,
                CatalogNumber = $"ABC{random.Next(1000,9999)}BO{random.Next(100, 999)}",
                Name = $"Planta{DateTime.Now.Ticks}",
                PlantType = $"Best{DateTime.Now.Ticks}",
                FoodType = $"Test{DateTime.Now.Ticks}",
                Quantity = random.Next(0, 1000),
                IsEdible = true
            };
            return newPlant;
        }
        private Plant CreateNewInvalidPlant()
        {
            var random = new Random();
            var newPlant = new Plant()
            {
                Id = (int)DateTime.Now.Ticks,
                CatalogNumber = $"ABC{random.Next(1000, 9999)}@@@!",
                Name = $"Test Invalid Name {DateTime.Now.Ticks}",
                PlantType = $"Test  {DateTime.Now.Ticks}",
                FoodType = "Don't know",
                Quantity = -1,
                IsEdible = true
            };
            return newPlant;
        }

        [Test]
        public async Task AddPlantAsync_ShouldAddNewPlant()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            // Act
            await plantsManager.AddAsync(newPlant);
            var plantDB = await dbContext.Plants.FirstOrDefaultAsync(s=> s.Id == newPlant.Id);
            // Assert
            Assert.IsNotNull(plantDB);
            Assert.AreEqual(newPlant, plantDB);

        }

        [Test]
        public async Task AddPlantAsync_TryToAddPlantWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newPlant = CreateNewInvalidPlant();
            // Act
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await plantsManager.AddAsync(newPlant));
            // Assert
            Assert.AreEqual("Invalid plant!", ex.Message);

        }

        [Test]
        public async Task DeletePlantAsync_WithValidCatalogNumber_ShouldRemovePlantFromDb()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            // Act
            await plantsManager.DeleteAsync(newPlant.CatalogNumber);
            var plantDB = await dbContext.Plants.FirstOrDefaultAsync(s=> s.CatalogNumber == newPlant.CatalogNumber);
            // Assert
            Assert.IsNull(plantDB);
        }
        [Test]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task DeletePlantAsync_TryToDeleteWithNullOrWhiteSpaceCatalogNumber_ShouldThrowException(string invalidCatalogNumber)
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            // Act
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await plantsManager.DeleteAsync(invalidCatalogNumber));
            // Assert
            Assert.AreEqual("Catalog number cannot be empty.", ex.Message);
        }

        [Test]
        public async Task GetAllAsync_WhenPlantsExist_ShouldReturnAllPlants()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            var newPlant1 = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            await plantsManager.AddAsync(newPlant1);
            // Act
            var plantDB = await plantsManager.GetAllAsync();

            // Assert
            Assert.IsNotNull(plantDB);
            Assert.AreEqual(plantDB.Count(), 2);
            Assert.AreEqual(newPlant,plantDB.First());
            Assert.AreEqual(newPlant1, plantDB.Last());
        }

        [Test]
        public async Task GetAllAsync_WhenNoPlantsExist_ShouldThrowKeyNotFoundException()
        {
            // Act
            var ex  = Assert.ThrowsAsync<KeyNotFoundException>(async () => await plantsManager.GetAllAsync());
            // Assert
            Assert.AreEqual("No plant found.", ex.Message);
        }

        [Test]
        public async Task SearchByFoodTypeAsync_WithExistingFoodType_ShouldReturnMatchingPlants()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            var newPlant1 = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            await plantsManager.AddAsync(newPlant1);
            // Act
            var result = await plantsManager.SearchByFoodTypeAsync(newPlant.FoodType);
            // Assert
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(newPlant, result.First()); 
        }
        [Test]
        public async Task SearchByFoodTypeAsync_WithNonExistingFoodType_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidFoodType = "invalidType";
            var newPlant = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.SearchByFoodTypeAsync(invalidFoodType));
            // Assert
            Assert.AreEqual("No plant found with the given food type.", ex.Message);
        }

        [Test]
        public async Task GetSpecificAsync_WithValidCatalogNumber_ShouldReturnPlant()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            // Act
            var result = await plantsManager.GetSpecificAsync(newPlant.CatalogNumber);
            var plantDB = await dbContext.Plants.FirstOrDefaultAsync( s=> s.CatalogNumber == newPlant.CatalogNumber );
            // Assert
            Assert.IsNotNull(plantDB);
            Assert.AreEqual(plantDB, result);  
        }
        [Test]
        public async Task GetSpecificAsync_WithInvalidCatalogNumber_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidCatalogNumber = "invalidCatalogNumber";
            var newPlant = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.GetSpecificAsync(invalidCatalogNumber));
            // Assert
            Assert.AreEqual("No plant found with catalog number: invalidCatalogNumber", ex.Message);
        }

        [Test]
        public async Task UpdateAsync_WithValidPlant_ShouldUpdatePlant()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            var newPlant1 = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            await plantsManager.AddAsync(newPlant1);
            var updatedPlant = newPlant;
            updatedPlant.Name = "UPDATED NAME";
            // Act
            await plantsManager.UpdateAsync(updatedPlant);
            var plantDB = await dbContext.Plants.FirstOrDefaultAsync( s=> s.Name == updatedPlant.Name );
            // Assert
            Assert.IsNotNull(plantDB);
            Assert.AreEqual(updatedPlant, plantDB);
        }
        [Test]
        public async Task UpdateAsync_WithInvalidPlant_ShouldThrowValidationException()
        {
            // Arrange
            var newPlant = CreateNewValidPlant();
            await plantsManager.AddAsync(newPlant);
            // Act
            var ex = Assert.ThrowsAsync<ValidationException>(() => plantsManager.UpdateAsync(new Plant()));
            // Assert
            Assert.AreEqual("Invalid plant!", ex.Message);
        }
    }
}
