using System;
using DarkRift;

namespace PartyMobileModels
{
	public class Player : IDarkRiftSerializable
	{
		public string username;
		public void Deserialize(DeserializeEvent e)
		{
			username = e.Reader.ReadString();
		}

		public void Serialize(SerializeEvent e)
		{
			e.Writer.Write(username);
		}
	}
}
