using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HotChocolate.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindbox.ExceptionsHandling.GraphQL;
using Moq;

namespace Mindbox.ExceptionsHandling.Tests;

[TestClass]
public class GraphQLHttpAgnosticResultSerializerTests
{
	private readonly GraphQLHttpAgnosticResultSerializer _serializer = new();

	[TestMethod]
	public void GetStatusCode_QueryResult_OK()
	{
		var executionResult = new QueryResult(new Dictionary<string, object?>(), null);

		var statusCode = _serializer.GetStatusCode(executionResult);

		Assert.AreEqual(HttpStatusCode.OK, statusCode);
	}

	[TestMethod]
	public void GetStatusCode_DeferredQueryResult_OK()
	{
		var executionResult = new DeferredQueryResult(
			new QueryResult(new Dictionary<string, object?>(), null),
			EmptyAsyncEnumerable());

		var statusCode = _serializer.GetStatusCode(executionResult);

		Assert.AreEqual(HttpStatusCode.OK, statusCode);
	}

	[TestMethod]
	public void GetStatusCode_BatchQueryResultQueryResult_OK()
	{
		var executionResult = new BatchQueryResult(EmptyAsyncEnumerable, null);

		var statusCode = _serializer.GetStatusCode(executionResult);

		Assert.AreEqual(HttpStatusCode.OK, statusCode);
	}

	[TestMethod]
	public void GetStatusCode_Other_InternalServerError()
	{
		var executionResult = new Mock<IExecutionResult>().Object;

		var statusCode = _serializer.GetStatusCode(executionResult);

		Assert.AreEqual(HttpStatusCode.InternalServerError, statusCode);
	}

	private static async IAsyncEnumerable<IQueryResult> EmptyAsyncEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}