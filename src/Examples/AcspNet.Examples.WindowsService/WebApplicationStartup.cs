﻿namespace Simplify.Web.Examples.WindowsService
{
	public class WebApplicationStartup
	{
		public void Run()
		{
			WebApp.Start<Startup>("http://localhost:8080");
		}
	}
}