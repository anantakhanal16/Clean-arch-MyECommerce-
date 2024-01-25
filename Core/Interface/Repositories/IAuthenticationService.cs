using Core.Entites.Identity;

namespace Core.Interface.Repositories
{
    public  interface IAuthenticationService
    {
        Task<Status> LoginAsync(UserLogin model);

        Task<Status> RegistrationAsync(RegistrationModel model);

        Task LogoutAsync();

    }
}
