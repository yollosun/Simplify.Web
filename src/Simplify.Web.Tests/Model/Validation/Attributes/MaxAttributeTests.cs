using System;
using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes;

[TestFixture]
public class MaxAttributeTests : AttributesTestBase
{
	public const int MaxValue = 12;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MaxAttribute(MaxValue);

	[Test]
	public void Validate_BelowMaxValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(10);
	}
	
	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(12);
	}
	
	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = 15;
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' required maximum value is {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
	
	[Test]
	public void Validate_NullValue_NoExceptions()
	{
		// Act & Assert
		TestAttributeForValidValue(null);
	}
	
	[Test]
	public void Validate_DifferentTypes_ExceptionThrown()
	{
		// Assign

		var value = 15.2;
		var defaultMessage = "Type mismatch. The maximum value and property value should be of the same type.";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
	
	[Test]
	public void Validate_ValueTypeNotInheritIComparable_ExceptionThrown()
	{
		// Assign

		var value = new object();
		var defaultMessage = $"The type of specified property value must be inherited from {typeof(IComparable)}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
}