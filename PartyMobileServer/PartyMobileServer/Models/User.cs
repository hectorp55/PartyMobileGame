using DarkRift.Server;

namespace PartyMobileServer.Models
{
	public class User
	{
		public string Username { get; set; }
		public IClient Client { get; set; }
	}
}
