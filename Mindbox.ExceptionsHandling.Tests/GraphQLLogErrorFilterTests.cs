using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindbox.ExceptionsHandling.GraphQL;
using Moq;

namespace Mindbox.ExceptionsHandling.Tests;

[TestClass]
public class GraphQLLogErrorFilterTests
{
	private static readonly LogLevel _matchedLogLevel = LogLevel.Critical;
	private readonly LoggerStub _loggerStub = new();

	[TestMethod]
	public void OnError_WithException_MatchedLogLevel()
	{
		var initialError = new Error("SomeMessage", exception: new Exception("MessageFromException"));

		OnError(initialError);

		var (logLevel, exception, message) = _loggerStub.GetSingleLoggedMessage();
		Assert.AreEqual(_matchedLogLevel, logLevel);
		Assert.AreEqual(initialError.Exception, exception);
		Assert.AreEqual("MessageFromException", message);
	}

	[TestMethod]
	public void OnError_WithSerializationException_Error()
	{
		var initialError = new Error(
			"SomeMessage",
			exception: new HotChocolate.Types.SerializationException(
				"MessageFromException",
				new Mock<HotChocolate.Types.IType>().Object));

		OnError(initialError);

		var (logLevel, exception, message) = _loggerStub.GetSingleLoggedMessage();
		Assert.AreEqual(LogLevel.Error, logLevel);
		Assert.AreEqual(null, exception);
		Assert.AreEqual("SomeMessage", message);
	}

	[TestMethod]
	public void OnError_WithoutException_Error()
	{
		var initialError = new Error("SomeMessage");

		OnError(initialError);

		var (logLevel, exception, message) = _loggerStub.GetSingleLoggedMessage();
		Assert.AreEqual(LogLevel.Error, logLevel);
		Assert.AreEqual(null, exception);
		Assert.AreEqual("SomeMessage", message);
	}

	private void OnError(IError error)
	{
		var exceptionCategoryMock = new Mock<IExceptionCategory>();
		exceptionCategoryMock.SetupGet(category => category.LogLevel).Returns(_matchedLogLevel);

		var exceptionCategoryMatcherMock = new Mock<IExceptionCategoryMatcher>();
		exceptionCategoryMatcherMock
			.Setup(matcher => matcher.GetCategory(It.IsAny<Exception>()))
			.Returns(exceptionCategoryMock.Object);

		var loggerFactoryMock = new Mock<ILoggerFactory>();
		loggerFactoryMock
			.Setup(factory => factory.CreateLogger(It.IsAny<string>()))
			.Returns(_loggerStub);

		var filter = new GraphQLLogErrorFilter(exceptionCategoryMatcherMock.Object, loggerFactoryMock.Object);

		filter.OnError(error);
	}

	private class LoggerStub : ILogger
	{
		private readonly List<(LogLevel, Exception?, string)> _loggedMessages = new();

		public IDisposable BeginScope<TState>(TState state) => throw new NotSupportedException();

		public bool IsEnabled(LogLevel logLevel) => true;

		public void Log<TState>(
			LogLevel logLevel,
			EventId eventId,
			TState state,
			Exception? exception,
			Func<TState, Exception?, string> formatter)
		{
			_loggedMessages.Add((logLevel, exception, formatter(state, exception)));
		}

		public (LogLevel, Exception?, string) GetSingleLoggedMessage() => _loggedMessages.Single();
	}
}