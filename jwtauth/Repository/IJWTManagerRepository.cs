using jwtauth.Models;

namespace jwtauth.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users user);
    }
}
