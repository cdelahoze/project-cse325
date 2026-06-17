using System;
using System.Threading.Tasks;
using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.Tests
{
    [TestClass]
    public class WorkoutServiceTests
    {
        [TestMethod]
        public async Task AddAsync_ShouldAddWorkoutToList()
        {
            // Arrange
            var service = new WorkoutService();
            var workout = new Workout
            {
                ExerciseType = "Running",
                DurationMinutes = 30,
                Date = DateTime.Today,
                Notes = "Morning run"
            };

            // Act
            await service.AddAsync(workout);
            var workouts = await service.GetAllAsync();

            // Assert
            Assert.AreEqual(1, workouts.Count);
            Assert.AreEqual("Running", workouts[0].ExerciseType);
            Assert.AreEqual(30, workouts[0].DurationMinutes);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnCorrectWorkout()
        {
            // Arrange
            var service = new WorkoutService();
            var workout = new Workout
            {
                ExerciseType = "Swimming",
                DurationMinutes = 45,
                Date = DateTime.Today
            };
            await service.AddAsync(workout);

            // Act
            var result = await service.GetAsync(workout.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(workout.Id, result.Id);
            Assert.AreEqual("Swimming", result.ExerciseType);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldModifyExistingWorkout()
        {
            // Arrange
            var service = new WorkoutService();
            var workout = new Workout
            {
                ExerciseType = "Cycling",
                DurationMinutes = 60,
                Date = DateTime.Today
            };
            await service.AddAsync(workout);

            // Act
            workout.ExerciseType = "Strength Training";
            workout.DurationMinutes = 50;
            await service.UpdateAsync(workout);

            var updated = await service.GetAsync(workout.Id);

            // Assert
            Assert.IsNotNull(updated);
            Assert.AreEqual("Strength Training", updated.ExerciseType);
            Assert.AreEqual(50, updated.DurationMinutes);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveWorkout()
        {
            // Arrange
            var service = new WorkoutService();
            var workout = new Workout
            {
                ExerciseType = "Yoga",
                DurationMinutes = 20,
                Date = DateTime.Today
            };
            await service.AddAsync(workout);

            // Act
            await service.DeleteAsync(workout.Id);
            var workouts = await service.GetAllAsync();

            // Assert
            Assert.AreEqual(0, workouts.Count);
        }
    }
}
