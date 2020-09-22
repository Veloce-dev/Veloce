using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Veloce.Engine.Http
{
	public class HttpServerContext
	{
		public HttpListenerContext RawContext { get; private set; }

		public RequestHttpContext Request { get; private set; }

		public ResponseHttpContext Response { get; private set; }

		public HttpServerContext(HttpListenerContext rawContext)
		{
			RawContext = rawContext;
			Request = new RequestHttpContext(rawContext);
			Response = new ResponseHttpContext(rawContext);
		}

		public class RequestHttpContext
		{
			private HttpListenerContext RawContext;

			public string Method => RawContext.Request.HttpMethod.ToUpper();

			public string Path => RawContext.Request.Url.LocalPath;

			public object Body { get; set; }

			public RequestHttpContext(HttpListenerContext rawContext)
			{
				RawContext = rawContext;
			}

			public async Task<byte[]> ReadBodyAsync()
			{
				var data = new List<byte>();
				var buf = new byte[256];
				int readLength;
				do
				{
					readLength = await RawContext.Request.InputStream.ReadAsync(buf, 0, 256);
					if (readLength > 0)
					{
						data.AddRange(buf.Take(readLength));
					}
				}
				while (readLength > 0);
				return data.ToArray();
			}
		}

		public class ResponseHttpContext
		{
			private HttpListenerContext RawContext;

			public ResponseHttpContext(HttpListenerContext rawContext)
			{
				RawContext = rawContext;
			}

			public async Task WriteAsync(int statusCode, byte[] data)
			{
				RawContext.Response.StatusCode = statusCode;
				await RawContext.Response.OutputStream.WriteAsync(data, 0, data.Length);
			}

			public async Task WriteAsync(int statusCode, string message)
			{
				RawContext.Response.StatusCode = statusCode;
				var buf = Encoding.UTF8.GetBytes(message);
				await RawContext.Response.OutputStream.WriteAsync(buf, 0, buf.Length);
			}

			public async Task WriteJsonAsync(int statusCode, object jsonSource)
			{
				RawContext.Response.ContentType = "application/json";
				RawContext.Response.StatusCode = statusCode;
				var buf = Encoding.UTF8.GetBytes(JToken.FromObject(jsonSource).ToString());
				await RawContext.Response.OutputStream.WriteAsync(buf, 0, buf.Length);
			}
		}
	}
}
