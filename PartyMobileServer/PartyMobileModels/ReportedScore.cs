using DarkRift;

namespace PartyMobileModels
{
	public class ReportedScore : IDarkRiftSerializable
	{
		public double score;
		public void Deserialize(DeserializeEvent e)
		{
			score = e.Reader.ReadDouble();
		}

		public void Serialize(SerializeEvent e)
		{
			e.Writer.Write(score);
		}
	}
}
