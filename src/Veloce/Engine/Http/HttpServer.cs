using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Veloce.Engine.Http
{
	public class HttpServer
	{
		public event Action<Exception> ConnectionErrorEvent;

		private Func<HttpServerContext, CancellationToken, Task> ConnectionHandler;

		private CancellationTokenSource Cancellation;

		private HttpListener Listener;

		private Task ListenTask;

		private List<HttpListenerContext> Connections = new List<HttpListenerContext>();

		public HttpServer(Func<HttpServerContext, CancellationToken, Task> connectionHandler)
		{
			ConnectionHandler = connectionHandler;
		}

		public void Start(int port)
		{
			if (Cancellation != null)
			{
				throw new InvalidOperationException("server is already started");
			}
			Cancellation = new CancellationTokenSource();
			Listener = new HttpListener();
			Listener.Prefixes.Add($"http://*:{port}/");
			Listener.Start();
			ListenTask = ListenLoop();
			Console.WriteLine("[HTTP] server started");
		}

		private async Task ListenLoop()
		{
			try
			{
				Console.WriteLine("[HTTP] listen loop started");
				while (!Cancellation.IsCancellationRequested)
				{
					try
					{
						var connection = await Listener.GetContextAsync();
						Connections.Add(connection);
						var connectionTask = OnConnected(connection);
						await Task.Delay(1, Cancellation.Token);
					}
					catch (HttpListenerException)
					{
						// listener is stopped
					}
				}
				Console.WriteLine("[HTTP] listen loop stopped");
			}
			catch (Exception ex)
			{
				Console.WriteLine("--- Unexpected Exception ---");
				Console.WriteLine(ex);
			}
		}

		private async Task OnConnected(HttpListenerContext connection)
		{
			try
			{
				Console.WriteLine("[HTTP] connected");
				try
				{
					await ConnectionHandler(new HttpServerContext(connection), Cancellation.Token);
				}
				catch (Exception ex)
				{
					if (ConnectionErrorEvent != null)
					{
						ConnectionErrorEvent(ex);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("--- Unexpected Exception ---");
				Console.WriteLine(ex);
			}
			finally
			{
				connection.Response.Close();
				Connections.Remove(connection);
				Console.WriteLine("[HTTP] disconnected");
			}
		}

		public async Task StopAsync()
		{
			if (ListenTask == null)
			{
				throw new InvalidOperationException("server is not started");
			}

			Console.WriteLine("[HTTP] server stopping ...");
			Listener.Stop();
			Cancellation.Cancel();

			while (Listener.IsListening || !ListenTask.IsCompleted || Connections.Count > 0)
			{
				await Task.Delay(1);
			}

			Console.WriteLine("[HTTP] server stopped");

			Listener = null;
			ListenTask = null;
			Cancellation = null;
		}
	}
}
