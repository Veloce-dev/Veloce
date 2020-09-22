using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Veloce.Engine.Http;

namespace Veloce.Engine
{
	public class PluginApiManager
	{
		private Dictionary<string, PluginSession> _Sessions = new Dictionary<string, PluginSession>();
		private object SessionsLock;

		public IReadOnlyList<PluginSession> Sessions => _Sessions.Values.ToList();

		public PluginApiManager(HttpRouteManager routeManager)
		{
			// register HTTP routes
			routeManager.Routes.Add(new HttpRoute("POST", "/plugin/session/generate", GenerateSessionHandler));
			routeManager.Routes.Add(new HttpRoute("POST", "/plugin/session/keep", KeepSessionHandler));
			routeManager.Routes.Add(new HttpRoute("POST", "/plugin/content/update", UpdateContentHandler));
		}

		private async Task GenerateSessionHandler(HttpServerContext ctx, CancellationToken ct)
		{
			var body = (JToken)ctx.Request.Body;
			if (body == null || !(body is JObject)) body = JObject.Parse("{}");

			if (body["content"] == null)
			{
				await ctx.Response.WriteJsonAsync(400, new { error = "invalid_parameter", paramName = "content" });
				return;
			}

			var content = new PluginContent();
			var capability = new List<PluginCapability> { };

			if (body["capability"] != null)
			{

			}

			var key = Guid.NewGuid().ToString();
			var expirationTime = DateTime.Now + TimeSpan.FromSeconds(10);
			var session = new PluginSession(expirationTime, content, capability);
			lock (SessionsLock)
			{
				_Sessions.Add(key, session);
			}

			await ctx.Response.WriteJsonAsync(200, new { key });
		}

		private async Task KeepSessionHandler(HttpServerContext ctx, CancellationToken ct)
		{
			var body = (JToken)ctx.Request.Body;
			if (body == null || !(body is JObject)) body = JObject.Parse("{}");

			if (body["key"] == null)
			{
				await ctx.Response.WriteJsonAsync(400, new { error = "invalid_parameter", paramName = "key" });
				return;
			}

			await ctx.Response.WriteJsonAsync(200, new { message = "keep session" });
		}

		private async Task UpdateContentHandler(HttpServerContext ctx, CancellationToken ct)
		{
			var body = (JToken)ctx.Request.Body;
			if (body == null || !(body is JObject)) body = JObject.Parse("{}");

			if (body["key"] == null)
			{
				await ctx.Response.WriteJsonAsync(400, new { error = "invalid_parameter", paramName = "key" });
				return;
			}

			await ctx.Response.WriteJsonAsync(200, new { message = "update content" });
		}
	}
}
