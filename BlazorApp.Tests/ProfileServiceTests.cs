using System.Threading.Tasks;
using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class ProfileServiceTests
    {
        [TestMethod]
        public async Task GetAsync_ShouldReturnNullInitially()
        {
            // Arrange
            var service = new ProfileService();

            // Act
            var profile = await service.GetAsync();

            // Assert
            Assert.IsNull(profile);
        }

        [TestMethod]
        public async Task SaveAsync_ShouldSaveProfile()
        {
            // Arrange
            var service = new ProfileService();
            var profile = new Profile
            {
                Name = "John Doe",
                InitialWeightKg = 80,
                HeightCm = 180,
                Age = 30
            };

            // Act
            await service.SaveAsync(profile);
            var savedProfile = await service.GetAsync();

            // Assert
            Assert.IsNotNull(savedProfile);
            Assert.AreEqual("John Doe", savedProfile.Name);
            Assert.AreEqual(80m, savedProfile.InitialWeightKg);
            Assert.AreEqual(180, savedProfile.HeightCm);
            Assert.AreEqual(30, savedProfile.Age);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldClearProfile()
        {
            // Arrange
            var service = new ProfileService();
            var profile = new Profile
            {
                Name = "Jane Doe",
                InitialWeightKg = 60,
                HeightCm = 165,
                Age = 25
            };
            await service.SaveAsync(profile);

            // Act
            await service.DeleteAsync();
            var clearedProfile = await service.GetAsync();

            // Assert
            Assert.IsNull(clearedProfile);
        }
    }
}
