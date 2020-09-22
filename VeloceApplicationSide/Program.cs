using System;
using System.Threading.Tasks;

namespace VeloceApplicationSide
{
	public class Program
	{
		private static async Task TestHttpServer()
		{
			Console.WriteLine("HTTP Server");
			var httpServer = new Veloce.Engine.Http.HttpServer((ctx, ct) => {
				return Task.CompletedTask;
			});
			httpServer.Start(64500);
			Console.WriteLine("Press any key to stop ...");
			Console.ReadLine();
			await httpServer.StopAsync();
		}

		private static async Task TestApp()
		{
			Console.WriteLine("Veloce application");
			var app = new Veloce.Application();
			app.Start();
			Console.WriteLine("Press any key to stop ...");
			Console.ReadLine();
			await app.StopAsync();
		}

		public static async Task Main(string[] args)
		{
			// await TestHttpServer();
			await TestApp();
		}
	}
}
