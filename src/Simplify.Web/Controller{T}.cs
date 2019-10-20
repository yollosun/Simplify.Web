﻿#nullable disable

using Simplify.DI;
using Simplify.Web.Model;

namespace Simplify.Web
{
	/// <summary>
	/// Synchronous model controllers base class
	/// </summary>
	public abstract class Controller<T> : SyncControllerBase
		where T : class
	{
		private T _model;

		/// <summary>
		/// Gets the model of current request.
		/// </summary>
		/// <value>
		/// The current request model.
		/// </value>
		public virtual T Model => _model ??= Resolver.Resolve<IModelHandler>().Process<T>(Resolver);
	}
}