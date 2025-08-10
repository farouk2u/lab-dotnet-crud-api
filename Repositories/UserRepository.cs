using dotnet_crud_api.Models;

namespace dotnet_crud_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_users);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User> CreateAsync(User user)
        {
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(User user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                _users[index] = user;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var index = _users.FindIndex(u => u.Id == id);
            if (index != -1)
            {
                _users.RemoveAt(index);
            }
            return Task.CompletedTask;
        }
    }
}
