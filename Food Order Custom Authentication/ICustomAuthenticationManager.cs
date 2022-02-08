namespace Food_Order_Custom_Authentication
{
    public interface ICustomAuthenticationManager
    {
        IDictionary<string, string> Tokens { get; }
        string Authenticate(String email, string password);
        bool Unauthenticate(String token);
    }
}
