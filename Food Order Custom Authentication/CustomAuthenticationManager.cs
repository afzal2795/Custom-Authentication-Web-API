using Food_Order_Custom_Authentication.Services;

namespace Food_Order_Custom_Authentication
{
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly ITokenService _tokenService;
        public CustomAuthenticationManager(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public readonly Dictionary<string, string> Users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" }
        };
        private readonly Dictionary<string, string> tokens = new Dictionary<string, string>();

        public IDictionary<string, string> Tokens => tokens;
        //public string Authenticate(string email, string password)
        //{
        //    if (!Users.Any(u => u.Key == email && u.Value == password))
        //        return null;
        //    //use encrypted key 2.43 and 5.00
        //    var token = Guid.NewGuid().ToString();
        //    tokens.Add(token, email);
        //    return token;

        //}

        public string Authenticate(string email, string password)
        {
            if (!Users.Any(u => u.Key == email && u.Value == password))
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
