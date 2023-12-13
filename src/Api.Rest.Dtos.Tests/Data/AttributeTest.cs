﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2022                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.Dtos.Tests.Data
{
	#region usings

	using System;
	using System.Collections;
	using System.Xml;
	using AutoFixture;
	using NUnit.Framework;
	using Zeiss.PiWeb.Api.Rest.Dtos.Data;
	using Attribute = Zeiss.PiWeb.Api.Core.Attribute;

	#endregion

	[TestFixture]
	public class AttributeTest
	{
		#region members

		private static readonly Fixture Fixture = new Fixture();

		#endregion

		#region methods

		[Test]
		public void Test_Ctr_For_Null()
		{
			// arrange

			// act
			var attribute = new Attribute( 1, null );

			// assert
			Assert.That( attribute.Value, Is.Null );
			Assert.That( attribute.RawValue, Is.Null );
		}

		[Test]
		[TestCase( "" )]
		[TestCase( "foo" )]
		public void Test_Ctr_For_Value( string value )
		{
			// arrange

			// act
			var attribute = new Attribute( 1, value );

			// assert
			Assert.That( attribute.Value, Is.EqualTo( value ) );
			Assert.That( attribute.RawValue, Is.Null );
		}

		[Test]
		[TestCaseSource( nameof( CreateValidRawValueTestCases ) )]
		public void Test_Ctr_For_RawValue( object rawValue, string expectedValue )
		{
			// arrange

			// act
			var attribute = new Attribute( 1, rawValue );

			Assert.That( attribute.Value, Is.EqualTo( expectedValue ) );
			Assert.That( attribute.RawValue, Is.EqualTo( rawValue ) );
		}

		[Test]
		public void Test_Ctr_For_RawValue_ThrowsException()
		{
			Assert.That( () => new Attribute( 1, Guid.NewGuid() ), Throws.ArgumentException );
		}

		[Test]
		public void Test_Compare_Attributes_With_Different_Keys()
		{
			// arrange
			var xAttribute = new Attribute( Fixture.Create<ushort>(), "foo" );
			var yAttribute = new Attribute( Fixture.Create<ushort>(), "foo" );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.False );

			Assert.That( xAttribute == yAttribute, Is.False );
			Assert.That( xAttribute != yAttribute, Is.True );
		}

		[Test]
		public void Test_Compare_Attribute_With_Value_And_Attribute_With_RawValue()
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, 42 );
			var yAttribute = new Attribute( attributeKey, "foo" );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.False );

			Assert.That( xAttribute == yAttribute, Is.False );
			Assert.That( xAttribute != yAttribute, Is.True );
		}

		[Test]
		public void Test_Compare_Attributes_With_Different_RawValue_Types()
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, 42 );
			var yAttribute = new Attribute( attributeKey, 1.23 );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.False );

			Assert.That( xAttribute == yAttribute, Is.False );
			Assert.That( xAttribute != yAttribute, Is.True );
		}

		[Test]
		[TestCase( null, null, true )]
		[TestCase( "", "", true )]
		[TestCase( " ", " ", true )]
		[TestCase( "\t", "\t", true )]
		[TestCase( "Hello World", "Hello World", true )]
		[TestCase( null, "", false )]
		[TestCase( null, " ", false )]
		[TestCase( "", " ", false )]
		[TestCase( "Hello World", "", false )]
		[TestCase( "Hello World", " ", false )]
		[TestCase( "Hello World", "\t", false )]
		[TestCase( "Hello World", "hello World", false )]
		public void Test_Compare_String_Attributes( string x, string y, bool expectedResult )
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, x );
			var yAttribute = new Attribute( attributeKey, y );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.EqualTo( expectedResult ) );

			Assert.That( xAttribute == yAttribute, Is.EqualTo( expectedResult ) );
			Assert.That( xAttribute != yAttribute, Is.Not.EqualTo( expectedResult ) );
		}

		[Test]
		[TestCase( 1, 1, true )]
		[TestCase( 1, 2, false )]
		[TestCase( -1, -1, true )]
		[TestCase( -1, -2, false )]
		[TestCase( -1, 1, false )]
		[TestCase( 0, 0, true )]
		[TestCase( int.MinValue, int.MinValue, true )]
		[TestCase( int.MaxValue, int.MaxValue, true )]
		public void Test_Compare_Integer_Attributes( int x, int y, bool expectedResult )
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, x );
			var yAttribute = new Attribute( attributeKey, y );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.EqualTo( expectedResult ) );

			Assert.That( xAttribute == yAttribute, Is.EqualTo( expectedResult ) );
			Assert.That( xAttribute != yAttribute, Is.Not.EqualTo( expectedResult ) );
		}

		[Test]
		[TestCase( 1, 1, true )]
		[TestCase( 1, 2, false )]
		[TestCase( -1, -1, true )]
		[TestCase( -1, -2, false )]
		[TestCase( -1, 1, false )]
		[TestCase( 0, 0, true )]
		[TestCase( short.MinValue, short.MinValue, true )]
		[TestCase( short.MaxValue, short.MaxValue, true )]
		public void Test_Compare_Short_Attributes( short x, short y, bool expectedResult )
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, x );
			var yAttribute = new Attribute( attributeKey, y );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.EqualTo( expectedResult ) );

			Assert.That( xAttribute == yAttribute, Is.EqualTo( expectedResult ) );
			Assert.That( xAttribute != yAttribute, Is.Not.EqualTo( expectedResult ) );
		}

		[Test]
		[TestCase( 1, 1, true )]
		[TestCase( 1, 2, false )]
		[TestCase( 0.1, 0.1, true )]
		[TestCase( 0.1, 0.2, false )]
		[TestCase( -1, -1, true )]
		[TestCase( -1, -2, false )]
		[TestCase( -0.1, -0.1, true )]
		[TestCase( -0.1, -0.2, false )]
		[TestCase( -0.1, 0.1, false )]
		[TestCase( 0.0, 0.0, true )]
		[TestCase( double.NaN, 1, false )]
		[TestCase( double.NaN, double.NaN, false )]
		[TestCase( double.PositiveInfinity, 1, false )]
		[TestCase( double.NegativeInfinity, 1, false )]
		[TestCase( double.PositiveInfinity, double.PositiveInfinity, true )]
		[TestCase( double.NegativeInfinity, double.NegativeInfinity, true )]
		[TestCase( double.PositiveInfinity, double.NegativeInfinity, false )]
		[TestCase( double.NegativeInfinity, double.PositiveInfinity, false )]
		public void Test_Compare_Double_Attributes( double x, double y, bool expectedResult )
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, x );
			var yAttribute = new Attribute( attributeKey, y );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.EqualTo( expectedResult ) );

			Assert.That( xAttribute == yAttribute, Is.EqualTo( expectedResult ) );
			Assert.That( xAttribute != yAttribute, Is.Not.EqualTo( expectedResult ) );
		}

		[Test]
		[TestCase( "1", 1, true )]
		[TestCase( "1", 2, false )]
		[TestCase( "0.1", 0.1, true )]
		[TestCase( "0.1", 0.2, false )]
		[TestCase( "-1", -1, true )]
		[TestCase( "-1", -2, false )]
		[TestCase( "-0.1", -0.1, true )]
		[TestCase( "-0.1", -0.2, false )]
		[TestCase( "-0.1", 0.1, false )]
		[TestCase( "0.0", 0.0, true )]
		[TestCase( "", 0.0, false )]
		[TestCase( "foo", 0.0, false )]
		public void Equals_LeftFromStringRightFromDouble_ReturnsCorrectResult( string x, double y, bool expectedResult )
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, x );
			var yAttribute = new Attribute( attributeKey, y );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.EqualTo( expectedResult ) );

			Assert.That( xAttribute == yAttribute, Is.EqualTo( expectedResult ) );
			Assert.That( xAttribute != yAttribute, Is.Not.EqualTo( expectedResult ) );
		}

		[Test]
		[TestCase( 1, "1", true )]
		[TestCase( 1, "2", false )]
		[TestCase( 0.1, "0.1", true )]
		[TestCase( 0.1, "0.2", false )]
		[TestCase( -1, "-1", true )]
		[TestCase( -1, "-2", false )]
		[TestCase( -0.1, "-0.1", true )]
		[TestCase( -0.1, "-0.2", false )]
		[TestCase( -0.1, "0.1", false )]
		[TestCase( 0.0, "0.0", true )]
		[TestCase( double.NaN, "1", false )]
		[TestCase( double.PositiveInfinity, "1", false )]
		[TestCase( double.NegativeInfinity, "1", false )]
		[TestCase( 0.0, "", false )]
		[TestCase( 0.0, "foo", false )]
		public void Equals_LeftFromDoubleRightFromString_ReturnsCorrectResult( double x, string y, bool expectedResult )
		{
			// arrange
			var attributeKey = Fixture.Create<ushort>();
			var xAttribute = new Attribute( attributeKey, x );
			var yAttribute = new Attribute( attributeKey, y );

			// act, assert
			Assert.That( Equals( xAttribute, yAttribute ), Is.EqualTo( expectedResult ) );

			Assert.That( xAttribute == yAttribute, Is.EqualTo( expectedResult ) );
			Assert.That( xAttribute != yAttribute, Is.Not.EqualTo( expectedResult ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateCompareDateTimeAttributesTestCases ) )]
		public void Test_Compare_DateTime_Attributes( AttributeComparisonTestCase testCase )
		{
			// arrange

			// act, assert
			Assert.That( Equals( testCase.X, testCase.Y ), Is.EqualTo( testCase.ExpectedResult ) );

			Assert.That( testCase.X == testCase.Y, Is.EqualTo( testCase.ExpectedResult ) );
			Assert.That( testCase.X != testCase.Y, Is.Not.EqualTo( testCase.ExpectedResult ) );
		}

		[Test]
		public void Test_GetHashCode_IsSame_When_Key_And_Value_Are_Same()
		{
			// arrange
			var attribute1 = new Attribute( 1, "foo" );
			var attribute2 = new Attribute( 1, "foo" );

			// act
			var hashCode1 = attribute1.GetHashCode();
			var hashCode2 = attribute2.GetHashCode();

			// assert
			Assert.That( hashCode1, Is.EqualTo( hashCode2 ) );
		}

		[Test]
		public void Test_GetHashCode_IsSame_When_RawValues_Are_Different_But_Return_Same_Value()
		{
			// arrange
			var attribute1 = new Attribute( 1, 1.23 );
			var attribute2 = new Attribute( 1, "1.23" );

			// act
			var hashCode1 = attribute1.GetHashCode();
			var hashCode2 = attribute2.GetHashCode();

			// assert
			Assert.That( hashCode1, Is.EqualTo( hashCode2 ) );
		}

		[Test]
		public void Test_GetHashCode_IsDifferent_When_Keys_Are_Different()
		{
			var attribute1 = new Attribute( 1, "foo" );
			var attribute2 = new Attribute( 2, "foo" );

			// act
			var hashCode1 = attribute1.GetHashCode();
			var hashCode2 = attribute2.GetHashCode();

			// assert
			Assert.That( hashCode1, Is.Not.EqualTo( hashCode2 ) );
		}

		[Test]
		public void Test_GetHashCode_IsDifferent_When_Values_Are_Different()
		{
			var attribute1 = new Attribute( 1, "foo" );
			var attribute2 = new Attribute( 1, "bar" );

			// act
			var hashCode1 = attribute1.GetHashCode();
			var hashCode2 = attribute2.GetHashCode();

			// assert
			Assert.That( hashCode1, Is.Not.EqualTo( hashCode2 ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetStringValue( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetStringValue();

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedStringValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetIntValue( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetIntValue();

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedIntValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetDoubleValue( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetDoubleValue();

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedDoubleValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetDateValue( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetDateValue();

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedDateValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		public void Test_GetRawValue_From_Null_Value_For_String( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( string ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		public void Test_GetRawValue_From_AlphaNumeric_Value_For_String( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( string ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedStringValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		public void Test_GetRawValue_From_Integer_Value_For_String( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( string ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedStringValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		public void Test_GetRawValue_From_Double_Value_For_String( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( string ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedStringValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetRawValue_From_Datetime_Value_For_String( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( string ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedStringValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		public void Test_GetRawValue_From_Null_Value_For_Integer( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( int ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		public void Test_GetRawValue_From_AlphaNumeric_Value_For_Integer( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( int ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedIntValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		public void Test_GetRawValue_From_Integer_Value_For_Integer( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( int ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedIntValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		public void Test_GetRawValue_From_Double_Value_For_Integer( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( int ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedIntValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetRawValue_From_Datetime_Value_For_Integer( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( int ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedIntValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		public void Test_GetRawValue_From_Null_Value_For_Double( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( double ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		public void Test_GetRawValue_From_AlphaNumeric_Value_For_Double( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( double ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDoubleValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		public void Test_GetRawValue_From_Integer_Value_For_Double( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( double ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDoubleValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		public void Test_GetRawValue_From_Double_Value_For_Double( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( double ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDoubleValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetRawValue_From_Datetime_Value_For_Double( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( double ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDoubleValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateNullAttributeTestCases ) )]
		public void Test_GetRawValue_From_Null_Value_For_Datetime( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( DateTime ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateAlphaNumericAttributeTestCases ) )]
		public void Test_GetRawValue_From_AlphaNumeric_Value_For_Datetime( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( DateTime ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDateValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateIntegerAttributeTestCases ) )]
		public void Test_GetRawValue_From_Integer_Value_For_Datetime( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( DateTime ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDateValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateFloatAttributeTestCases ) )]
		public void Test_GetRawValue_From_Double_Value_For_Datetime( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( DateTime ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDateValue ) );
		}

		[Test]
		[TestCaseSource( nameof( CreateDateTimeAttributeTestCases ) )]
		public void Test_GetRawValue_From_Datetime_Value_For_Datetime( AttributeConversionTestCase testCase )
		{
			// arrange

			// act
			var result = testCase.Attribute.GetRawValue( typeof( DateTime ) );

			// assert
			Assert.That( result, Is.EqualTo( testCase.ExpectedRawValue ?? testCase.ExpectedDateValue ) );
		}

		[Test]
		public void Test_GetRawValue_For_Not_Supported_Type()
		{
			// arrange
			var attribute = new Attribute( 1, "foo" );

			// act
			var result = attribute.GetRawValue( typeof( Guid ) );

			// assert
			Assert.That( result, Is.Null );
		}

		[Test]
		public void Test_IsNull_For_Null()
		{
			// arrange

			// act
			var attribute = new Attribute( 1, null );

			// assert
			Assert.That( attribute.IsNull, Is.True );
		}

		[Test]
		[TestCase( "" )]
		[TestCase( "foo" )]
		public void Test_IsNull_For_Value( string value )
		{
			// arrange

			// act
			var attribute = new Attribute( 1, value );

			// assert
			Assert.That( attribute.IsNull, Is.False );
		}

		[Test]
		public void Test_IsNull_For_RawValue_As_String_Value()
		{
			// arrange

			// act
			var attribute = new Attribute( 1, (object)"foo" );

			// assert
			Assert.That( attribute.IsNull, Is.False );
		}

		[Test]
		[TestCaseSource( nameof( CreateValidRawValueTestCases ) )]
		public void Test_IsNull_For_RawValue( object rawValue, string expectedValue )
		{
			// arrange

			// act
			var attribute = new Attribute( 1, rawValue );

			// assert
			Assert.That( attribute.IsNull, Is.EqualTo( rawValue == null ) );
		}

		[Test]
		public void Test_ToString_Of_Empty_Attribute()
		{
			// arrange

			// act
			var attribute = new Attribute( 1, "" );

			// assert
			Assert.That( attribute.ToString(), Is.EqualTo( "K1: " ) );
		}

		[Test]
		public void Test_ToString_Of_Not_Empty_Attribute()
		{
			// arrange

			// act
			var attribute = new Attribute( 1, "foo" );

			// assert
			Assert.That( attribute.ToString(), Is.EqualTo( "K1: foo" ) );
		}

		#endregion

		#region helper methods

		private static IEnumerable CreateValidRawValueTestCases()
		{
			yield return new TestCaseData( null, null ) { TestName = "null" };
			yield return new TestCaseData( "", "" ) { TestName = "empty string" };
			yield return new TestCaseData( "foo", "foo" ) { TestName = "string value" };
			yield return new TestCaseData( 2, "2" ) { TestName = "integer value" };
			yield return new TestCaseData( (short)2, "2" ) { TestName = "short value" };
			yield return new TestCaseData( 1.23, "1.23" ) { TestName = "double value" };

			var dateTime = DateTime.Parse( "2015-03-09T19:12:00Z" ).ToUniversalTime();
			yield return new TestCaseData( dateTime, "2015-03-09T19:12:00Z" ) { TestName = "datetime" };
		}

		private static IEnumerable CreateCompareDateTimeAttributesTestCases()
		{
			var attributeKey = Fixture.Create<ushort>();
			var olderTime = new DateTime( 1985, 4, 16, 0, 0, 0, DateTimeKind.Utc );
			var newerTime = new DateTime( 1988, 4, 24, 0, 0, 0, DateTimeKind.Utc );

			yield return new AttributeComparisonTestCase { X = new Attribute( attributeKey, olderTime ), Y = new Attribute( attributeKey, olderTime ), ExpectedResult = true };
			yield return new AttributeComparisonTestCase { X = new Attribute( attributeKey, olderTime ), Y = new Attribute( attributeKey, newerTime ), ExpectedResult = false };
			yield return new AttributeComparisonTestCase { X = new Attribute( attributeKey, newerTime ), Y = new Attribute( attributeKey, olderTime ), ExpectedResult = false };
		}

		/// <remark>
		/// The null attribute is handled differently than other alpha numeric attributes because <see cref="Attribute.GetRawValue"/>
		/// for a null attribute, unlike other alpha numeric attributes, returns <see cref="Attribute.Value"/> rather than
		/// <see cref="Attribute._Value"/> which is <see langword="null" />.
		/// </remark>
		private static IEnumerable CreateNullAttributeTestCases()
		{
			// @formatter:off — disable formatter after this line

			// value: null
			yield return CreateTestCase( null,  null, null, null,    null, null,   "null" );

			// @formatter:on — enable formatter after this line
		}

		private static IEnumerable CreateAlphaNumericAttributeTestCases()
		{
			// @formatter:off — disable formatter after this line

			// value: "foo"
			yield return CreateTestCase(         "foo", null, null, "foo", null,  null,"\"foo\"" );
			yield return CreateTestCase( (object)"foo", null, null, "foo", null, "foo","\"foo\" (object)" );

			// value: "" (empty string)
			yield return CreateTestCase(         "", null, null,    "" ,   null,  null,   "\"\"" );
			yield return CreateTestCase( (object)"", null, null,    "" ,   null,    "",   "\"\" (object)" );

			// @formatter:on — enable formatter after this line
		}

		private static IEnumerable CreateIntegerAttributeTestCases()
		{
			// @formatter:off — disable formatter after this line

			// value: 1 as integer, short and string
			yield return CreateTestCase(           1, 1,  1.0,    "1", null,   1, "1" );
			yield return CreateTestCase(    (short)1, 1,  1.0,    "1", null,   1, "1 (short)" );
			yield return CreateTestCase(         "1", 1,  1.0,    "1", null,null, "\"1\"" );
			yield return CreateTestCase( (object)"1", 1,  1.0,    "1", null, "1", "\"1\" (object)" );

			// @formatter:on — enable formatter after this line
		}

		private static IEnumerable CreateFloatAttributeTestCases()
		{
			// @formatter:off — disable formatter after this line

			// value: 1.0 as double and string
			yield return CreateTestCase(           1.0,       1,  1.0,     "1", null,    1.0, "1.0" );
			yield return CreateTestCase(         "1.0",    null,  1.0,   "1.0", null,   null, "\"1.0\"" );
			yield return CreateTestCase( (object)"1.0",    null,  1.0,   "1.0", null,  "1.0", "\"1.0\" (object)" );

			// value: -1.78 as double and string
			yield return CreateTestCase(           -1.78, null, -1.78, "-1.78", null,   -1.78, "-1.78" );
			yield return CreateTestCase(         "-1.78", null, -1.78, "-1.78", null,    null, "\"-1.78\"" );
			yield return CreateTestCase( (object)"-1.78", null, -1.78, "-1.78", null, "-1.78", "\"-1.78\" (object)" );

			// value: special double values like NaN and Infinity
			yield return CreateTestCase( double.NaN,              null,               double.NaN,       "NaN", null, double.NaN,              "NaN" );
			yield return CreateTestCase( double.PositiveInfinity, null,  double.PositiveInfinity,  "Infinity", null, double.PositiveInfinity, "Infinity" );
			yield return CreateTestCase( double.NegativeInfinity, null,  double.NegativeInfinity, "-Infinity", null, double.NegativeInfinity, "-Infinity" );
			yield return CreateTestCase( "NaN",                   null,               double.NaN,       "NaN", null, null,                    "\"NaN\"" );
			yield return CreateTestCase( "Infinity",              null,  double.PositiveInfinity,  "Infinity", null, null,                    "\"Infinity\"" );
			yield return CreateTestCase( "-Infinity",             null,  double.NegativeInfinity, "-Infinity", null, null,                    "\"-Infinity\"" );
			yield return CreateTestCase( (object)"NaN",           null,               double.NaN,       "NaN", null, "NaN",                   "\"NaN\" (object)" );
			yield return CreateTestCase( (object)"Infinity",      null,  double.PositiveInfinity,  "Infinity", null, "Infinity",              "\"Infinity\" (object)" );
			yield return CreateTestCase( (object)"-Infinity",     null,  double.NegativeInfinity, "-Infinity", null, "-Infinity",             "\"-Infinity\" (object)" );

			// @formatter:on — enable formatter after this line
		}

		private static IEnumerable CreateDateTimeAttributeTestCases()
		{
			// @formatter:off — disable formatter after this line

			// value: date
			var universalTime = DateTime.Parse( "2015-03-09T19:12:00Z" ).ToUniversalTime();
			var universalTimeAsString = XmlConvert.ToString( universalTime, XmlDateTimeSerializationMode.RoundtripKind );

			var unspecifiedTime = new DateTime( universalTime.Ticks, DateTimeKind.Unspecified );
			var unspecifiedTimeAsString = XmlConvert.ToString( unspecifiedTime, XmlDateTimeSerializationMode.RoundtripKind );

			yield return CreateTestCase( universalTime,         null, null, universalTimeAsString,   universalTime, universalTime,         $"utc: {universalTimeAsString}" );
			yield return CreateTestCase( unspecifiedTime,       null, null, unspecifiedTimeAsString, universalTime, unspecifiedTime,       $"unspecified: {unspecifiedTimeAsString}" );

			yield return CreateTestCase( universalTimeAsString,           null, null, universalTimeAsString,     universalTime, null,                    $"\"{universalTimeAsString}\"" );
			yield return CreateTestCase( unspecifiedTimeAsString,         null, null, unspecifiedTimeAsString,   universalTime, null,                    $"\"{universalTimeAsString}\"" );
			yield return CreateTestCase( (object)universalTimeAsString,   null, null, universalTimeAsString,     universalTime, universalTimeAsString,   $"\"{universalTimeAsString}\" (object)" );
			yield return CreateTestCase( (object)unspecifiedTimeAsString, null, null, unspecifiedTimeAsString,   universalTime, unspecifiedTimeAsString, $"\"{unspecifiedTimeAsString}\" (object)" );

			// @formatter:on — enable formatter after this line
		}

		private static AttributeConversionTestCase CreateTestCase(
			object attributeValue,
			int? expectedIntValue, double? expectedDoubleValue, string expectedStringValue, DateTime? expectedDateValue,
			object expectedRawValue,
			string displayText = null )
		{
			return new AttributeConversionTestCase
			{
				Attribute = new Attribute( 1, attributeValue ),
				ExpectedIntValue = expectedIntValue,
				ExpectedDoubleValue = expectedDoubleValue,
				ExpectedStringValue = expectedStringValue,
				ExpectedDateValue = expectedDateValue,
				ExpectedRawValue = expectedRawValue,
				DisplayText = displayText
			};
		}

		private static AttributeConversionTestCase CreateTestCase(
			string attributeValue,
			int? expectedIntValue, double? expectedDoubleValue, string expectedStringValue, DateTime? expectedDateValue,
			object expectedRawValue,
			string displayText = null )
		{
			return new AttributeConversionTestCase
			{
				Attribute = new Attribute( 1, attributeValue ),
				ExpectedIntValue = expectedIntValue,
				ExpectedDoubleValue = expectedDoubleValue,
				ExpectedStringValue = expectedStringValue,
				ExpectedDateValue = expectedDateValue,
				ExpectedRawValue = expectedRawValue,
				DisplayText = displayText
			};
		}

		#endregion

		#region class AttributeComparisonTestCase

		public class AttributeComparisonTestCase
		{
			#region properties

			public Attribute X { get; set; }

			public Attribute Y { get; set; }

			public bool ExpectedResult { get; set; }

			#endregion

			#region methods

			public override string ToString()
			{
				return $"{PrintAttribute( X )}, {PrintAttribute( Y )}";

				string PrintAttribute( Attribute attribute )
				{
					if( attribute.RawValue is CatalogEntryDto catalogEntry )
						return $"Catalog entry {catalogEntry.Key}: {catalogEntry}";

					return attribute.Value ?? "null";
				}
			}

			#endregion
		}

		#endregion
	}
}