using System.Net;

using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;

namespace Mindbox.ExceptionsHandling.GraphQL;

public class GraphQLHttpAgnosticResultFormatter : DefaultHttpResponseFormatter
{
	protected override HttpStatusCode OnDetermineStatusCode(
		IQueryResult result,
		FormatInfo format,
		HttpStatusCode? proposedStatusCode)
	{
		return result switch
		{
			// This is required for our frontend
			QueryResult => HttpStatusCode.OK,
			_ => HttpStatusCode.InternalServerError
		};
	}
}