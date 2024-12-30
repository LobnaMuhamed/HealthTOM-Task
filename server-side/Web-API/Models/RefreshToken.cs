namespace Web_API.Models
{
	public class RefreshToken
	{
		public int TokenId { get; set; }
		public int UserId { get; set; }
		public string Token { get; set; }
		public DateOnly ExpirationDate { get; set; }
	}
}
