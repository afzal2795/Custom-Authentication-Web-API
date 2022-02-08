using Food_Order_Custom_Authentication.TokenAuthentication;
using Microsoft.Extensions.Caching.Memory;

namespace Food_Order_Custom_Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCache cache;

        public TokenService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        //public string FetchToken(string token)
        //{
        //    string generatedToken = string.Empty;

        //    if (!cache.TryGetValue(token, out generatedToken))
        //    {
        //        var tokenmodel = this.GenerateToken();
        //        var options = new MemoryCacheEntryOptions()
        //                            .SetAbsoluteExpiration(
        //                                TimeSpan.FromSeconds(tokenmodel.ExpiresIn)
        //                             );

        //        cache.Set(tokenmodel.Value, tokenmodel.Value, options);

        //        generatedToken = tokenmodel.Value;
        //    }

        //    return generatedToken;
        //}

        public string FetchToken()
        {
            var generatedToken = Guid.NewGuid().ToString();
            var options = new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(
                                    TimeSpan.FromSeconds(60)
                                    );

            cache.Set(generatedToken, generatedToken, options);

            return generatedToken;
        }

        private Token GenerateToken()
        {
            var token = new Token(Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(1), 60);
            return token;
        }

        public string ValidateToken(string token)
        {
            var tokenValue = string.Empty;
            if(cache.TryGetValue(token, out tokenValue))
               return tokenValue;
            return null;
        }

        public bool DestroyToken(string token)
        {
            string tokenValue = string.Empty;
            var tokenExists = cache.TryGetValue(token, out tokenValue);
            if (tokenExists)
            {
                cache.Remove(token);
                return true;
            }
            return false;
        }
    }
}
