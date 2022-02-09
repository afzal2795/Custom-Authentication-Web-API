using Food_Order_Custom_Authentication.TokenAuthentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Memory;

namespace Food_Order_Custom_Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCache _cache;
        private readonly IDataProtector _protector;

        public TokenService(IMemoryCache cache, IDataProtectionProvider protector)
        {
            _cache = cache;
            _protector = protector.CreateProtector(GetType().FullName);
        }

        public string FetchToken()
        {
            var generatedToken = Guid.NewGuid().ToString();
            var options = new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(
                                    TimeSpan.FromMinutes(20)
                                    );

            _cache.Set(generatedToken, generatedToken, options);
            var encryptedToken = _protector.Protect(generatedToken);
            return encryptedToken;
        }

        public bool ValidateToken(string token)
        {
            token = _protector.Unprotect(token);
            var tokenValue = string.Empty;
            if(_cache.TryGetValue(token, out tokenValue))
               return true;
            return false;
        }

        public bool DestroyToken(string token)
        {
            token = _protector.Unprotect(token);
            string tokenValue = string.Empty;
            var tokenExists = _cache.TryGetValue(token, out tokenValue);
            if (tokenExists)
            {
                _cache.Remove(token);
                return true;
            }
            return false;
        }
    }
}
