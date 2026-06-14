using BlazorApp.Models;

namespace BlazorApp.Services
{
    public class ProfileService
    {
        private Profile? _profile;

        public Task<Profile?> GetAsync()
        {
            return Task.FromResult(_profile);
        }

        public Task SaveAsync(Profile profile)
        {
            _profile = new Profile
            {
                Id = profile.Id == Guid.Empty ? Guid.NewGuid() : profile.Id,
                Name = profile.Name,
                InitialWeightKg = profile.InitialWeightKg,
                HeightCm = profile.HeightCm,
                Age = profile.Age,
                MainGoal = profile.MainGoal,
                ActivityLevel = profile.ActivityLevel,
                Notes = profile.Notes
            };

            return Task.CompletedTask;
        }

        public Task DeleteAsync()
        {
            _profile = null;
            return Task.CompletedTask;
        }
    }
}