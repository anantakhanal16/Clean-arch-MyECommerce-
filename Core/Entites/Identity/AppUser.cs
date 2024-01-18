using Microsoft.AspNetCore.Identity;


namespace Core.Entites.Identity
{
    public  class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        
        public Address Address { get; set; }
    }
}
