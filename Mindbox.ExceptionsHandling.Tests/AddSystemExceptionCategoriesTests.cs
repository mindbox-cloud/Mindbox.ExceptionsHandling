using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.ExceptionsHandling.Tests;

[TestClass]
public class AddSystemExceptionCategoriesTests
{
	[TestMethod]
	public void UnauthorizedAccessException()
	{
		var category = GetCategory(new UnauthorizedAccessException());

		Assert.IsNotNull(category);
		Assert.IsInstanceOfType(category, typeof(UnauthorizedAccessExceptionCategory));
		Assert.AreEqual(LogLevel.Warning, category!.LogLevel);
	}

	[TestMethod]
	public void TimeoutException()
	{
		var category = GetCategory(new TimeoutException());

		Assert.IsNotNull(category);
		Assert.IsInstanceOfType(category, typeof(ServiceUnavailableExceptionCategory));
		Assert.AreEqual(LogLevel.Warning, category!.LogLevel);
	}

	[TestMethod]
	public void UnknownException()
	{
		var category = GetCategory(new Exception());

		Assert.IsNull(category);
	}

	private IExceptionCategory? GetCategory(Exception exception)
	{
		var serviceCollection = new ServiceCollection().AddSystemExceptionCategories();
		var exceptionCategories = serviceCollection.BuildServiceProvider().GetServices<IExceptionCategory>();

		return exceptionCategories.SingleOrDefault(category => category.DoesMatchTopException(exception));
	}
}