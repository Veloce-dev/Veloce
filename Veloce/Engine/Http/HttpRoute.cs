using System;
using System.Threading;
using System.Threading.Tasks;

namespace Veloce.Engine.Http
{
	public class HttpRoute
	{
		public string Method { get; set; }

		public string Path { get; set; }

		public Func<HttpServerContext, CancellationToken, Task> Handler { get; set; }

		public HttpRoute(string method, string path, Func<HttpServerContext, CancellationToken, Task> handler)
		{
			Method = method;
			Path = path;
			Handler = handler;
		}
	}
}
