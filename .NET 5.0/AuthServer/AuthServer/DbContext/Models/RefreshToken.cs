namespace SportDataAuth.DbContext.Models
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
