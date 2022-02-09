namespace Food_Order_Custom_Authentication.TokenAuthentication
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Token(string value, DateTime expiryDate)
        {
            Value = value;
            ExpiryDate = expiryDate;
        }
    }
}
