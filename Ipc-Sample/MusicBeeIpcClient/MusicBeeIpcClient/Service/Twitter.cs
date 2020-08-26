using System.Net.Http;

namespace MusicBeeIpcClient.Service
{
	public class Twitter
	{
		private static readonly string _ck = "";
		private static readonly string _cs = "";
		private static readonly string _at = "";
		private static readonly string _ats = "";

		public Twist.Twitter Twist { get; private set; } = new Twist.Twitter(_ck, _cs, _at, _ats, new HttpClient(new HttpClientHandler()));

		public Twitter() { }

	}
}
