using Issues.Services.Contracts;

namespace Issues.Services
{
    public class ServiceManager : IServiceManager
    {
        private IAuthService _authService;
        public ServiceManager(IAuthService authService)
        {
            _authService = authService;
        }

        public IAuthService AuthService => _authService;
    }
}
