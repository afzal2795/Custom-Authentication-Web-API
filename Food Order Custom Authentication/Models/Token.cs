namespace Food_Order_Custom_Authentication.TokenAuthentication
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ExpiresIn { get; set; }
        public Token(string value, DateTime expiryDate, int expiresIn)
        {
            Value = value;
            ExpiryDate = expiryDate;
            ExpiresIn = expiresIn;
        }
    }
}
