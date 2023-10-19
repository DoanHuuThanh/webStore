using doanhuuthanh_web.ViewModel.System.Users;

namespace doanhuuthanh_web.addminWeb.Services
{
    public interface IUserApiClient
    {
        Task<string> Authencate(LoginRequest request);
    }
}
