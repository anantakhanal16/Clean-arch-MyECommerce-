using Core.Entites.Identity;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class UserAuthentication : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAuthentication(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); 
            return user?.UserName;
        }

        public async Task<Status> LoginAsync(UserLogin model)
        {
            var status = new Status();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid username";
                    return status;
                }

                // Match password
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid password";
                    return status;
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

                if (signInResult.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name)
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    status.StatusCode = 1;
                    status.Message = "Logged In Success";
                    return status;
                }
                else if (signInResult.IsLockedOut)
                {
                    status.StatusCode = 0;
                    status.Message = "User locked out";
                    return status;
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on login";
                    return status;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            var status = new Status();

            try
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);

                if (userExists != null)
                {
                    status.StatusCode = 0;
                    status.Message = "User already exists";
                    return status;
                }

                var user = new AppUser
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    UserName = model.DisplayName,
                    Name = model.DisplayName,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    status.StatusCode = 0;
                    status.Message = "User creation failed. Errors: " + string.Join(", ", result.Errors.Select(e => e.Description));
                    return status;
                }

                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                status.StatusCode = 1;
                status.Message = "User has registered successfully";
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
