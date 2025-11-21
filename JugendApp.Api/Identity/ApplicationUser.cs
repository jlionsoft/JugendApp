using JugendApp.SharedModels.Person;
using Microsoft.AspNetCore.Identity;

namespace JugendApp.Api.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }

}
