#nullable disable

using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Abstractions;

public class DefaultExceptionCategory : IExceptionCategory
{
	private DefaultExceptionCategory()
	{
	}

	public static DefaultExceptionCategory Instance { get; } = new();

	public string Name => ExceptionCategoryNames.Default;
	public LogLevel LogLevel => LogLevel.Critical;

	public bool DoesMatchTopException(Exception exception)
	{
		throw new NotSupportedException(
			"DefaultExceptionCategory should not be used for matching exceptions. " +
			"Do not register this category in DI container.");
	}
}