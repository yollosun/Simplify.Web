﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Providers response writer
	/// </summary>
	public class ResponseWriter : IResponseWriter
	{
		/// <summary>
		/// Writes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public void Write(string data, HttpResponse response)
		{
			// TODO check correctness
			response.WriteAsync(data).Wait();
		}

		/// <summary>
		/// Writes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public void Write(byte[] data, HttpResponse response)
		{
			//response.Write(data);
			throw new NotImplementedException();
		}

		/// <summary>
		/// Writes the specified data asynchronously.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public Task WriteAsync(string data, HttpResponse response)
		{
			return response.WriteAsync(data);
		}

		/// <summary>
		/// Writes the specified data asynchronously.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public Task WriteAsync(byte[] data, HttpResponse response)
		{
			//return response.WriteAsync(data);
			throw new NotImplementedException();
		}
	}
}