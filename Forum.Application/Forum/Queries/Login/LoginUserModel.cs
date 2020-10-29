namespace Forum.Application.Forum.Queries.Login
{
	public class LoginUserModel
	{
		public int Id { get; set; }
		public string EmailAddress { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
	}
}
