﻿using System;
using System.Collections.Generic;
using AcspNet.Modules;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	public class AcspNetContextTests
	{
		private Mock<IOwinContext> _owinContext;

		[SetUp]
		public void Initialize()
		{
			_owinContext = new Mock<IOwinContext>();

			_owinContext.SetupGet(x => x.Response).Returns(new Mock<IOwinResponse>().Object);
			_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mywebsite"));
			_owinContext.SetupGet(x => x.Request.Uri).Returns(new Uri("http://localhost/mywebsite/"));
			_owinContext.SetupGet(x => x.Request.Query).Returns(new ReadableStringCollection(new Dictionary<string, string[]>()));
		}

		[Test]
		public void Constructor_NormalContext_SetCorrectly()
		{
			// Act
			var context = new AcspNetContext(_owinContext.Object);

			// Assert

			Assert.AreEqual(_owinContext.Object, context.Context);
			Assert.AreEqual(_owinContext.Object.Request, context.Request);
			Assert.AreEqual(_owinContext.Object.Response, context.Response);
			Assert.AreEqual(_owinContext.Object.Request.Query, context.Query);
			Assert.AreEqual("http://localhost/mywebsite/", context.SiteUrl);
			Assert.AreEqual("/mywebsite/", context.VirtualPath);
		}

		[Test]
		public void Constructor_NoVirtualPath_Empty()
		{
			// Assign
			_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));

			// Act
			var context = new AcspNetContext(_owinContext.Object);

			// Assert
			Assert.AreEqual("", context.VirtualPath);
		}

		[Test]
		public void Constructor_LocalhostWithVirtualPathAndSegmentsWithQueryString_ParsedCorrectly()
		{
			// Assign

			_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mywebsite"));
			_owinContext.SetupGet(x => x.Request.Uri).Returns(new Uri("http://localhost/mywebsite/test?act=foo"));

			// Act
			var context = new AcspNetContext(_owinContext.Object);

			// Assert

			Assert.AreEqual("http://localhost/mywebsite/", context.SiteUrl);
		}

		[Test]
		public void Constructor_NormalPath_ParsedCorrectly()
		{
			// Assign

			_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
			_owinContext.SetupGet(x => x.Request.Uri).Returns(new Uri("http://mywebsite.com"));

			// Act
			var context = new AcspNetContext(_owinContext.Object);

			// Assert

			Assert.AreEqual("http://mywebsite.com", context.SiteUrl);
		}

		[Test]
		public void Constructor_NormalPathAndSegmentsWithQueryString_ParsedCorrectly()
		{
			// Assign

			_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
			_owinContext.SetupGet(x => x.Request.Uri).Returns(new Uri("http://mywebsite.com/test/?act=foo"));

			// Act
			var context = new AcspNetContext(_owinContext.Object);

			// Assert

			Assert.AreEqual("http://mywebsite.com", context.SiteUrl);
		}
	}
}