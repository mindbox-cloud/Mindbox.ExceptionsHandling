using System;
using HotChocolate;

namespace Mindbox.ExceptionsHandling.GraphQL;

public static class ErrorBuilderExtensions
{
	public static IErrorBuilder SetExceptionData(this IErrorBuilder errorBuilder, Exception? exception)
	{
		if (exception is null) return errorBuilder;

		var data = exception.Data;
		foreach (var dataKey in data.Keys)
		{
			if (dataKey is not null)
			{
				errorBuilder.SetExtension(dataKey.ToString()!, data[dataKey]);
			}
		}

		return errorBuilder;
	}
}