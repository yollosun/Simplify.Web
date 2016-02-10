﻿using System.Threading.Tasks;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Represent web-page processor
	/// </summary>
	public interface IPageProcessor
	{
		/// <summary>
		/// Processes (build web-page and send to client, process current page state) the current web-page
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		Task ProcessPage(IDIContainerProvider containerProvider, IOwinContext context);
	}
}