﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2016                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.HttpClient.RawData.Filter.Conditions
{
	#region usings

	using System;
	using System.Xml;
	using Zeiss.PiWeb.Api.Rest.Common.Data.FilterString.Tree;

	#endregion

	public class DateTimeCompareFilterCondition : FilterCondition
	{
		#region members

		private readonly DateTimeAttributes _Attribute;
		private readonly CompareOperation _Operation;
		private readonly DateTime? _Value;

		#endregion

		#region constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeCompareFilterCondition"/> class.
		/// </summary>
		public DateTimeCompareFilterCondition( DateTimeAttributes attribute, CompareOperation operation, DateTime? value )
		{
			_Attribute = attribute;
			_Operation = operation;
			_Value = value;
		}

		#endregion

		#region methods

		/// <inheritdoc />
		public override IFilterTree BuildFilterTree()
		{
			string valueString = null;
			if( _Value.HasValue )
				valueString = XmlConvert.ToString( _Value.Value, XmlDateTimeSerializationMode.RoundtripKind );

			var attributeName = FilterHelper.GetAttributeName( _Attribute );
			var operatorTokenType = FilterHelper.GetOperatorTokenType( _Operation );

			var valueTree = FilterTree.MakeValue( valueString );
			return FilterHelper.MakeComparison( operatorTokenType, attributeName, valueTree );
		}

		#endregion
	}
}