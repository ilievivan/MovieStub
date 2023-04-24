using MovieStub.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace MovieStub.Domain.Identity
{
    public class MovieStubAppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public virtual ShoppingCart UserCart { get; set; }
    }
}
