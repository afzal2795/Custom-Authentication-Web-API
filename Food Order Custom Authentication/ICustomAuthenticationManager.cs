using Food_Order_Custom_Authentication.Models;

namespace Food_Order_Custom_Authentication
{
    public interface ICustomAuthenticationManager
    {
        string Authenticate(String email, string password, User user);
        bool Unauthenticate(String token);
    }
}
