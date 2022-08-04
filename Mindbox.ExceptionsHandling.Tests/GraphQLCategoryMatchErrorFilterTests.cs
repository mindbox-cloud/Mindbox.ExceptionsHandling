using System;
using HotChocolate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindbox.ExceptionsHandling.GraphQL;
using Moq;

namespace Mindbox.ExceptionsHandling.Tests;

[TestClass]
public class GraphQLCategoryMatchErrorFilterTests
{
	private const string MatchedCategoryName = "MatchedCategory";

	[TestMethod]
	public void OnError_WihCode_NotModified()
	{
		var initialError = new Error("SomeMessage", "SomeCode");

		var processedError = OnError(initialError);

		Assert.AreEqual("SomeCode", processedError.Code);
		Assert.AreEqual(initialError, processedError);
	}

	[TestMethod]
	public void OnError_WithoutCode_WithoutException_InvalidRequest()
	{
		var initialError = new Error("SomeMessage");

		var processedError = OnError(initialError);

		Assert.AreEqual(ExceptionCategoryNames.InvalidRequest, processedError.Code);
		Assert.AreEqual("SomeMessage", processedError.Message);
	}

	[TestMethod]
	public void OnError_WithoutCode_WithException_MatchedCategory()
	{
		var initialError = new Error("SomeMessage", exception: new Exception("MessageFromException"));

		var processedError = OnError(initialError);

		Assert.AreEqual(MatchedCategoryName, processedError.Code);
		Assert.AreEqual("MessageFromException", processedError.Message);
	}

	private IError OnError(IError error)
	{
		var exceptionCategoryMock = new Mock<IExceptionCategory>();
		exceptionCategoryMock.SetupGet(category => category.Name).Returns(MatchedCategoryName);

		var exceptionCategoryMatcherMock = new Mock<IExceptionCategoryMatcher>();
		exceptionCategoryMatcherMock
			.Setup(matcher => matcher.GetCategory(It.IsAny<Exception>()))
			.Returns(exceptionCategoryMock.Object);

		var filter = new GraphQLCategoryMatchErrorFilter(exceptionCategoryMatcherMock.Object);

		return filter.OnError(error);
	}
}