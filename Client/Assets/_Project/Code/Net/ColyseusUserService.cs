using System.Threading.Tasks;

namespace AleshaCoder.Net
{
    public sealed class ColyseusUserService : IUserService
    {
        private string _userId;
        public string UserId => _userId;

        public Task<string> EnsureUserAsync()
        {
            if (string.IsNullOrEmpty(_userId))
                _userId = System.Guid.NewGuid().ToString("N"); // демо
            return Task.FromResult(_userId);
        }
    }
}