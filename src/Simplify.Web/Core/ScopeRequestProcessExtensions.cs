﻿using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Provides request execution by Simplify.Web
	/// </summary>
	public static class ScopeRequestProcessExtensions
	{
		/// <summary>
		/// Processes the request inside specified scope.
		/// </summary>
		/// <param name="scope">The scope.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public static RequestHandlingResult ProcessRequest(this ILifetimeScope scope, HttpContext context)
		{
			// Run request process pipeline
			return scope.Resolver.Resolve<IRequestHandler>().ProcessRequest(scope.Resolver, context);
		}
	}
}