using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Veloce.Engine;
using Veloce.Engine.Http;

namespace Veloce
{
	public class Application
	{
		public IReadOnlyList<PluginSession> PluginSessions => PluginApi.Sessions;

		private HttpServer Server;

		private HttpRouteManager RouteManager;

		private PluginApiManager PluginApi;

		public Application()
		{
			Server = new HttpServer(ConnectionHandler);
			Server.ConnectionErrorEvent += (ex) => {
				Console.WriteLine(">>> Connection Exception");
				ShowException(ex);
			};

			RouteManager = new HttpRouteManager();
			PluginApi = new PluginApiManager(RouteManager);
		}

		private static void ShowException(Exception ex)
		{
			Console.WriteLine($"Name: {ex.GetType().FullName}");
			Console.WriteLine("StackTrace:");
			Console.WriteLine(ex.StackTrace);
			if (ex.InnerException != null)
			{
				Console.WriteLine(">>> InnerException");
				ShowException(ex.InnerException);
			}
		}

		// ConnectionHandler will run as concurrent or parallel
		private async Task ConnectionHandler(HttpServerContext ctx, CancellationToken ct)
		{
			try
			{
				// parse the body as JSON string
				if (ctx.Request.Method == "POST" && ctx.RawContext.Request.ContentType == "application/json")
				{
					var bodyBuf = await ctx.Request.ReadBodyAsync();
					var bodyStr = Encoding.UTF8.GetString(bodyBuf);
					try
					{
						ctx.Request.Body = JToken.Parse(bodyStr);
					}
					catch (JsonReaderException)
					{
						await ctx.Response.WriteJsonAsync(400, new { error = "bad_request" });
						return;
					}
				}

				Console.WriteLine($"route: {ctx.Request.Method} {ctx.Request.Path.ToLower()}");

				// routing
				var routeCalled = await RouteManager.CallRoute(ctx, ct);
				if (!routeCalled)
				{
					Console.WriteLine("endpoint not found");
					await ctx.Response.WriteJsonAsync(404, new { error = "endpoint not found" });
				}
			}
			catch
			{
				await ctx.Response.WriteJsonAsync(500, new { error = "Server Error" });
				throw;
			}
		}

		public void Start()
		{
			Server.Start(64500);
		}

		public async Task StopAsync()
		{
			await Server.StopAsync();
		}
	}
}
