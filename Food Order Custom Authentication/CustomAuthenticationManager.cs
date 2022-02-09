using Food_Order_Custom_Authentication.Models;
using Food_Order_Custom_Authentication.Services;
using BC = BCrypt.Net.BCrypt;

namespace Food_Order_Custom_Authentication
{
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly ITokenService _tokenService;
        public CustomAuthenticationManager(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public string Authenticate(string email, string password, User user)
        {
            if (user == null || !BC.Verify(password, user.Password))
                return null;

            var token = _tokenService.FetchToken();
            return token;
        }

        public bool Unauthenticate(string token)
        {
            var filteredtoken = token.Replace("bearer" , String.Empty).Trim();
            var status = _tokenService.DestroyToken(filteredtoken);
            return status;
        }
    }
}
