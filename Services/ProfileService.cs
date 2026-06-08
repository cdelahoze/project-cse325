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
            _profile = profile;
            return Task.CompletedTask;
        }

        public Task DeleteAsync()
        {
            _profile = null;
            return Task.CompletedTask;
        }
    }
}
