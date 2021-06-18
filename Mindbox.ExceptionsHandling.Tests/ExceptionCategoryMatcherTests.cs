using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.ExceptionsHandling.Tests
{
	[TestClass]
	public class ExceptionCategoryMatcherTests
	{
		[TestMethod]
		public void Constructor_NullCategoriesList_ArgumentNullException()
		{
			var exception = Assert.ThrowsException<ArgumentNullException>(
				() => new ExceptionCategoryMatcher(null!));
			
			Assert.AreEqual("categories", exception.ParamName);
		}
		
		[TestMethod]
		public void Constructor_EmptyCategoriesList_ArgumentException()
		{
			var exception = Assert.ThrowsException<ArgumentException>(
				() => new ExceptionCategoryMatcher(new IExceptionCategory[0]));

			Assert.AreEqual("categories", exception.ParamName);
		}
		
		[TestMethod]
		public void TryGetTopCategory_FirstCategoryException_GotFirstExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category");
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionCategory));
			Assert.AreEqual(LogLevel.Error, category!.LogLevel);
		}
		
		[TestMethod]
		public void TryGetTopCategory_FirstSubCategoryException_GotFirstExceptionSubCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First subcategory");
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionSubCategory));
			Assert.AreEqual(LogLevel.Warning, category!.LogLevel);
		}
		
		[TestMethod]
		public void TryGetTopCategory_SecondCategoryException_GotOneOfSecondExceptionCategories()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Second category");
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(SecondExceptionCategory));
			Assert.AreEqual(LogLevel.Information, category!.LogLevel);
		}
		
		[TestMethod]
		public void TryGetTopCategory_FirstCategoryExceptionAndSecondCategoryInnerException_GotFirstExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category", 
				new InvalidOperationException("Second category"));
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionCategory));
			Assert.AreEqual(LogLevel.Error, category!.LogLevel);
		}
		
		[TestMethod]
		public void TryGetTopCategory_UnknownException_GotNull()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new Exception("Unknown");
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNull(category);
		}
		
		[TestMethod]
		public void TryGetTopCategory_UnknownExceptionAndFirstCategoryInnerException_GotNull()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Unknown", 
				new InvalidOperationException("First category"));
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNull(category);
		}
		
		[TestMethod]
		public void TryGetTopCategory_AggregateException_FirstCategoryInnerException_GotNull()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException", 
				new InvalidOperationException("First category"));
			
			var category = categoryMatcher.TryGetTopCategory(exception);

			Assert.IsNull(category);
		}
		
		[TestMethod]
		public void GetCategory_FirstCategoryException_GotFirstExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category");
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionCategory));
			Assert.AreEqual(LogLevel.Error, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_FirstSubCategoryException_GotFirstExceptionSubCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First subcategory");
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionSubCategory));
			Assert.AreEqual(LogLevel.Warning, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_SecondCategoryException_GotOneOfSecondExceptionCategories()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Second category");
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(SecondExceptionCategory));
			Assert.AreEqual(LogLevel.Information, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_FirstCategoryExceptionAndSecondCategoryInnerException_GotFirstExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category", 
				new InvalidOperationException("Second category"));
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionCategory));
			Assert.AreEqual(LogLevel.Error, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_UnknownException_GotDefaultExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new Exception("Unknown");
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(DefaultExceptionCategory));
			Assert.AreEqual(LogLevel.Critical, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_UnknownExceptionAndFirstCategoryInnerException_GotFirstExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Unknown", 
				new InvalidOperationException("First category"));
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionCategory));
			Assert.AreEqual(LogLevel.Error, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_AggregateException_FirstCategoryAndSecondCategoryInnerExceptions_GotFirstExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException",
				new InvalidOperationException("First category"), 
				new InvalidOperationException("Second category"));
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(FirstExceptionCategory));
			Assert.AreEqual(LogLevel.Error, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_AggregateException_UnknownAndSecondCategoryInnerExceptions_GotSecondExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException",
				new Exception("Unknown"), 
				new InvalidOperationException("Second category"));
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(SecondExceptionCategory));
			Assert.AreEqual(LogLevel.Information, category!.LogLevel);
		}
		
		[TestMethod]
		public void GetCategory_AggregateException_UnknownInnerExceptions_GotDefaultExceptionCategory()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException",
				new Exception("Unknown"), 
				new Exception("Unknown"));
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsNotNull(category);
			Assert.IsInstanceOfType(category, typeof(DefaultExceptionCategory));
			Assert.AreEqual(LogLevel.Critical, category!.LogLevel);
		}
		
		[TestMethod]
		public void HasFirstExceptionCategory_FirstCategoryException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category");
			
			Assert.IsTrue(categoryMatcher.HasCategory<FirstExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasFirstExceptionCategory_SecondCategoryException_ReturnsFalse()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Second category");
			
			Assert.IsFalse(categoryMatcher.HasCategory<FirstExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasFirstExceptionCategory_FirstSubCategoryException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First subcategory");

			Assert.IsTrue(categoryMatcher.HasCategory<FirstExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasFirstExceptionSubCategory_FirstSubCategoryException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First subcategory");

			Assert.IsTrue(categoryMatcher.HasCategory<FirstExceptionSubCategory>(exception));
		}
		
		[TestMethod]
		public void HasSecondExceptionCategory_SecondCategoryException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Second category");

			Assert.IsTrue(categoryMatcher.HasCategory<SecondExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasFirstExceptionCategory_FirstCategoryExceptionAndSecondCategoryInnerException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category", 
				new InvalidOperationException("Second category"));

			Assert.IsTrue(categoryMatcher.HasCategory<FirstExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasSecondExceptionCategory_FirstCategoryExceptionAndSecondCategoryInnerException_ReturnsFalse()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("First category", 
				new InvalidOperationException("Second category"));

			Assert.IsFalse(categoryMatcher.HasCategory<SecondExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasDefaultExceptionCategory_UnknownException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new Exception("Unknown");

			Assert.IsTrue(categoryMatcher.HasCategory<DefaultExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasDefaultExceptionCategory_UnknownExceptionAndFirstCategoryInnerException_ReturnsFalse()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Unknown", 
				new InvalidOperationException("First category"));

			Assert.IsFalse(categoryMatcher.HasCategory<DefaultExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasFirstExceptionCategory_UnknownExceptionAndFirstCategoryInnerException_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new InvalidOperationException("Unknown", 
				new InvalidOperationException("First category"));

			Assert.IsTrue(categoryMatcher.HasCategory<FirstExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasFirstExceptionCategory_AggregateException_FirstCategoryAndSecondCategoryInnerExceptions_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException",
				new InvalidOperationException("First category"), 
				new InvalidOperationException("Second category"));

			Assert.IsTrue(categoryMatcher.HasCategory<FirstExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasSecondExceptionCategory_AggregateException_UnknownAndSecondCategoryInnerExceptions_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException",
				new Exception("Unknown"), 
				new InvalidOperationException("Second category"));
			
			var category = categoryMatcher.GetCategory(exception);

			Assert.IsTrue(categoryMatcher.HasCategory<SecondExceptionCategory>(exception));
		}
		
		[TestMethod]
		public void HasDefaultExceptionCategory_AggregateException_UnknownInnerExceptions_ReturnsTrue()
		{
			var categoryMatcher = CreateExceptionCategoryMatcher();
			var exception = new AggregateException("AggregateException",
				new Exception("Unknown"), 
				new Exception("Unknown"));
			
			Assert.IsTrue(categoryMatcher.HasCategory<DefaultExceptionCategory>(exception));
		}

		private IExceptionCategoryMatcher CreateExceptionCategoryMatcher() => new ExceptionCategoryMatcher(
			new IExceptionCategory[]
			{
				new FirstExceptionCategory(exception => 
					exception is InvalidOperationException && exception.Message == "First category", 
					LogLevel.Error),
				new FirstExceptionSubCategory(exception => 
					exception is InvalidOperationException && exception.Message == "First subcategory", 
					LogLevel.Warning),
				new SecondExceptionCategory(exception => 
					exception is InvalidOperationException && exception.Message == "Second category", 
					LogLevel.Information),
				new SecondExceptionCategory(
					exception => exception is InvalidOperationException && exception.Message.Contains("Second category"), 
					LogLevel.None)
			});
		
		private class FirstExceptionCategory : ExceptionCategory
		{
			public FirstExceptionCategory(Func<Exception, bool> exceptionFilter, LogLevel logLevel) : base(exceptionFilter, logLevel)
			{
			}

			public override string Name => "FirstExceptionCategory";
		}
		
		private class FirstExceptionSubCategory : FirstExceptionCategory
		{
			public FirstExceptionSubCategory(Func<Exception, bool> exceptionFilter, LogLevel logLevel) : base(exceptionFilter, logLevel)
			{
			}
		}
		
		private class SecondExceptionCategory : ExceptionCategory
		{
			public SecondExceptionCategory(Func<Exception, bool> exceptionFilter, LogLevel logLevel) : base(exceptionFilter, logLevel)
			{
			}

			public override string Name => "SecondExceptionCategory";
		}
	}
}