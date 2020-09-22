using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Veloce.Engine.Http
{
	public class HttpRouteManager
	{
		public List<HttpRoute> Routes { get; private set; } = new List<HttpRoute>();

		public HttpRouteManager() { }

		public async Task<bool> CallRoute(HttpServerContext ctx, CancellationToken ct)
		{
			var route = Routes.Find((r) => r.Method == ctx.Request.Method && r.Path == ctx.Request.Path);
			if (route != null)
			{
				await route.Handler(ctx, ct);
			}
			return route != null;
		}
	}
}
