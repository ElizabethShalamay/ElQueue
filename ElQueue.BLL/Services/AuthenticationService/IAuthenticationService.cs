using ElQueue.BLL.Models;
using System.Threading.Tasks;

namespace ElQueue.BLL.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<UserBm> GetUserByName(string userName);

        Task<UserBm> SignInAsync(string login, string password);

        Task<UserBm> RegisterAsync(RegisterModel user);
    }
}
