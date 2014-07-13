﻿using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;
using AcspNet.Routing;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class ControllersAgentTests
	{
		private ControllersAgent _agent;
		private Mock<IControllersMetaStore> _metaStore;
		private IRouteMatcher _routeMatcher;

		[SetUp]
		public void Initialize()
		{
			_metaStore = new Mock<IControllersMetaStore>();
			_routeMatcher = new RouteMatcher();
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher);
		}

		[Test]
		public void GetStandartControllersMetaData_StandartControllerAndAll40xControllers_OnlyStandartReturned()
		{
			// Assign

			_metaStore.Setup(x => x.GetControllersMetaData())
				.Returns(new List<IControllerMetaData>
				{
					new ControllerMetaData(null),
					new ControllerMetaData(null, null, new ControllerRole(true)),
					new ControllerMetaData(null, null, new ControllerRole(false, true)),
					new ControllerMetaData(null, null, new ControllerRole(false, false, true))
				});

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher);

			// Act
			var items = _agent.GetStandartControllersMetaData().ToList();

			// Assert
			Assert.AreEqual(1, items.Count());
			Assert.IsNull(items.First().Role);
		}

		[Test]
		public void MatchControllerRoute_NoControllerRouteData_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null), "/test", "GET");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_GetControllerRouteGetMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/test"))), "/test", "GET");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_PostControllerRoutePostMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/test"))), "/test", "POST");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_PutControllerRoutePutMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, "/test"))), "/test", "PUT");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_DeleteControllerRouteDeleteMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, "/test"))), "/test", "DELETE");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_PostControllerRouteGetMethodMatched_Null()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/test"))), "/test", "GET");

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public void MatchControllerRoute_GetControllerRouteGetMethodNotMatched_NoSuccess()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/test"))), "/foo", "GET");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void MatchControllerRoute_UndefinedMethod_Null()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/test"))), "/foo", "FOO");

			// Assert
			Assert.IsNull(result);
		}

		//[Test]
		//public void IsNonAnyPageController_DefaultController_True()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object);
		//	var metaContainer = new ControllerMetaContainer(null,
		//		new ControllerExecParameters(null, null, 0, true));
		//	var metaContainer2 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar", 0, true));

		//	// Act & Assert

		//	Assert.IsTrue(agent.IsNonAnyPageController(metaContainer));
		//	Assert.IsTrue(agent.IsNonAnyPageController(metaContainer2));
		//}

		//[Test]
		//public void IsNonAnyPageController_SpecificPageController_True()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object);
		//	var metaContainer = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar"));
		//	var metaContainer2 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo"));

		//	// Act & Assert

		//	Assert.IsTrue(agent.IsNonAnyPageController(metaContainer));
		//	Assert.IsTrue(agent.IsNonAnyPageController(metaContainer2));
		//}

		//[Test]
		//public void IsNonAnyPageController_AnyPageController_False()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object);
		//	var metaContainer = new ControllerMetaContainer(null);
		//	var metaContainer2 = new ControllerMetaContainer(null, new ControllerExecParameters());
		//	var metaContainer3 = new ControllerMetaContainer(null, new ControllerExecParameters(""));

		//	// Act & Assert

		//	Assert.IsFalse(agent.IsNonAnyPageController(metaContainer));
		//	Assert.IsFalse(agent.IsNonAnyPageController(metaContainer2));
		//	Assert.IsFalse(agent.IsNonAnyPageController(metaContainer3));
		//}

		//[Test]
		//public void IsControllerCanBeExecutedOnCurrentPage_DefaultPage_CorrectControllersResult()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object);
		//	var metaContainer = new ControllerMetaContainer(null,
		//		new ControllerExecParameters(null, null, 0, true));
		//	var metaContainer2 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar", 0, true));
		//	var metaContainer3 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar"));
		//	var metaContainer4 = new ControllerMetaContainer(null);
		//	var metaContainer5 = new ControllerMetaContainer(null, new ControllerExecParameters());

		//	// Act & Assert

		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer2));
		//	Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer3));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer4));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer5));
		//}

		//[Test]
		//public void IsControllerCanBeExecutedOnCurrentPage_SpecifiedPage_CorrectControllersResult()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");
		//	var metaContainer = new ControllerMetaContainer(null,
		//		new ControllerExecParameters(null, null, 0, true));
		//	var metaContainer2 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar", 0, true));
		//	var metaContainer3 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar"));
		//	var metaContainer4 = new ControllerMetaContainer(null);
		//	var metaContainer5 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo"));
		//	var metaContainer6 = new ControllerMetaContainer(null, new ControllerExecParameters());

		//	// Act & Assert

		//	Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer));
		//	Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer2));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer3));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer4));
		//	Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer5));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer6));
		//}

		//[Test]
		//public void IsControllerCanBeExecutedOnCurrentPage_SpecifiedPageOnyAction_CorrectControllersResult()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo");
		//	var metaContainer = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo", "bar"));
		//	var metaContainer2 = new ControllerMetaContainer(null);
		//	var metaContainer3 = new ControllerMetaContainer(null,
		//		new ControllerExecParameters("foo"));

		//	// Act & Assert

		//	Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer2));
		//	Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer3));
		//}

		//[Test]
		//public void IsSecurityRulesViolated_NoSecurityRules_Ok()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object);
		//	var metaContainer = new ControllerMetaContainer(null);
		//	var metaContainer2 = new ControllerMetaContainer(null, null, new ControllerSecurity());

		//	// Act & Assert

		//	Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer));
		//	Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer2));
		//}

		//[Test]
		//public void IsSecurityRulesViolated_SecurityRulesSetNoHttpParameters_SecurityViolated()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object);
		//	var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(true));
		//	var metaContainer2 = new ControllerMetaContainer(null, null, new ControllerSecurity(false, true));
		//	var metaContainer3 = new ControllerMetaContainer(null, null, new ControllerSecurity(false, false, true));

		//	// Act & Assert

		//	Assert.AreEqual(SecurityViolationResult.AuthenticationRequired, agent.IsSecurityRulesViolated(metaContainer));
		//	Assert.AreEqual(SecurityViolationResult.RequestTypeViolated, agent.IsSecurityRulesViolated(metaContainer2));
		//	Assert.AreEqual(SecurityViolationResult.RequestTypeViolated, agent.IsSecurityRulesViolated(metaContainer3));
		//}

		//[Test]
		//public void IsSecurityRulesViolated_SecurityRulesSetAndHttpPostGetParametersIsCorrect_Ok()
		//{
		//	// Arrange

		//	var agent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, null, null, "GET");
		//	var agent2 = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, null, null, "POST");

		//	var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(false, true));
		//	var metaContainer2 = new ControllerMetaContainer(null, null, new ControllerSecurity(false, false, true));

		//	// Act & Assert

		//	Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer));
		//	Assert.AreEqual(SecurityViolationResult.Ok, agent2.IsSecurityRulesViolated(metaContainer2));
		//}

		//[Test]
		//public void IsSecurityRulesViolated_AuthenticationRequiredUserNotAuthenticated_AuthenticationRequiredResult()
		//{
		//	// Arrange

		//	var state = new Mock<IAuthenticationState>();
		//	state.SetupGet(x => x.IsAuthenticatedAsUser).Returns(false);

		//	var agent = new ControllerExecutionAgent(state.Object);

		//	var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(true));

		//	// Act & Assert

		//	Assert.AreEqual(SecurityViolationResult.AuthenticationRequired, agent.IsSecurityRulesViolated(metaContainer));
		//}

		//[Test]
		//public void IsSecurityRulesViolated_AuthenticationRequiredUserAuthenticated_Ok()
		//{
		//	// Arrange

		//	var state = new Mock<IAuthenticationState>();
		//	state.SetupGet(x => x.IsAuthenticatedAsUser).Returns(true);

		//	var agent = new ControllerExecutionAgent(state.Object);

		//	var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(true));

		//	// Act & Assert

		//	Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer));
		//}
	}
}