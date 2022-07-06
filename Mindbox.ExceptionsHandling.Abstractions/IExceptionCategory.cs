using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public interface IExceptionCategory
{
	string Name { get; }
	LogLevel LogLevel { get; }
	bool DoesMatchTopException(Exception exception);
}