﻿using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules
{
	/// <summary>
	/// Represent web context
	/// </summary>
	public interface IWebContext : IHideObjectMembers
	{
		/// <summary>
		/// Current web-site route, for example: "/" or "/user/delete/15"
		/// </summary>
		string Route { get; }

		/// <summary>
		/// Site root url, for example: http://mysite.com or http://localhost/mysite/
		/// </summary>
		string SiteUrl { get; }

		/// <summary>
		/// Gets the virtual path.
		/// </summary>
		/// <value>
		/// The virtual path.
		/// </value>
		string VirtualPath { get; }

		/// <summary>
		/// Gets the context for the current HTTP request.
		/// </summary>
		HttpContext Context { get; }

		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		HttpRequest Request { get; }

		/// <summary>
		/// Gets the response for the current HTTP request.
		/// </summary>
		HttpResponse Response { get; }

		// TODO check correct interface
		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		IQueryCollection Query { get; }

		// TODO check correct interface
		/// <summary>
		/// Gets the form data of post HTTP request.
		/// </summary>
		IFormCollection Form { get; }

		/// <summary>
		/// Gets a value indicating whether this request is ajax request.
		/// </summary>
		/// <value>
		/// <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
		/// </value>
		bool IsAjax { get; }
	}
}