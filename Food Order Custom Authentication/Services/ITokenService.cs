namespace Food_Order_Custom_Authentication.Services
{
    public interface ITokenService
    {
        string FetchToken();
        string ValidateToken(string token);
        bool DestroyToken(string token);
    }
}