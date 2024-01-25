using Microsoft.AspNetCore.Identity;


namespace Core.Entites.Identity
{
    public  class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
        public Address Address { get; set; }
    }
}
