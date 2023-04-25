using Microsoft.AspNetCore.Identity;

namespace ProjectDetailsAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
