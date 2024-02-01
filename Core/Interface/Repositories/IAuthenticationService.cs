using Core.Entites.Identity;

namespace Core.Interface.Repositories
{
    public  interface IAuthenticationService
    {
        Task<Status> LoginAsync(UserLogin model);
        Task<string> GetUserNameAsync(string userId);

        Task<Status> RegistrationAsync(RegistrationModel model);


        Task LogoutAsync();

    }
}
