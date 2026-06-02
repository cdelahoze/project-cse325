using BlazorApp.Models;

namespace BlazorApp.Services
{
    public class WorkoutService
    {
        private readonly List<Workout> _workouts = new();

        public Task<List<Workout>> GetAllAsync()
        {
            return Task.FromResult(_workouts.ToList());
        }

        public Task<Workout?> GetAsync(Guid id)
        {
            return Task.FromResult(_workouts.FirstOrDefault(w => w.Id == id));
        }

        public Task AddAsync(Workout workout)
        {
            _workouts.Add(workout);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Workout workout)
        {
            var idx = _workouts.FindIndex(w => w.Id == workout.Id);
            if (idx >= 0) _workouts[idx] = workout;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _workouts.RemoveAll(w => w.Id == id);
            return Task.CompletedTask;
        }
    }
}
