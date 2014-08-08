﻿using System.Web;

namespace AcspNet.Web
{
	/// <summary>
	/// Provides HTTP request handler
	/// </summary>
	public interface IRequestHandler
	{
		/// <summary>
		/// Processes the request.
		/// </summary>
		/// <param name="context">The context.</param>
		void ProcessRequest(HttpContextBase context);
	}
}