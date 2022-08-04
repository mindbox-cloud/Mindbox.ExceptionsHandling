using System.Net;

using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;

namespace Mindbox.ExceptionsHandling.GraphQL;

public class GraphQLHttpAgnosticResultSerializer : DefaultHttpResultSerializer
{
	public override HttpStatusCode GetStatusCode(IExecutionResult result)
	{
		return result switch
		{
			QueryResult => HttpStatusCode.OK,
			DeferredQueryResult => HttpStatusCode.OK,
			BatchQueryResult => HttpStatusCode.OK,
			_ => HttpStatusCode.InternalServerError
		};
	}
}