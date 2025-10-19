using System.Threading.Tasks;

namespace AleshaCoder.Net
{
    public interface IUserService
    {
        string UserId { get; }
        Task<string> EnsureUserAsync();
    }
}