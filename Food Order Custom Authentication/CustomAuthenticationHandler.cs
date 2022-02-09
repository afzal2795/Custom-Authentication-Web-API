using Food_Order_Custom_Authentication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace Food_Order_Custom_Authentication
{
    public class BasicAuthenticationOptions: AuthenticationSchemeOptions
    {

    }
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;
        private readonly ITokenService _tokenService;
        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomAuthenticationManager customAuthenticationManager,
            ITokenService tokenService): 
            base(options, loggerFactory, encoder, clock)
        {
            _customAuthenticationManager = customAuthenticationManager;
            _tokenService = tokenService;
        }
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.Fail("Unauthorized");

            if(!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Unauthorized");
            
            string token = authorizationHeader.Substring("bearer".Length).Trim();
            if(string.IsNullOrEmpty(token))
                return AuthenticateResult.Fail("Unauthorized");

            try
            {
                return ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
        }

        public AuthenticateResult ValidateToken(string token)
        {
            var validatedToken = _tokenService.ValidateToken(token);
            if (!validatedToken)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, token)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
