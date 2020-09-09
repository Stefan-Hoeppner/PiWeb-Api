﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss IMT (IZfM Dresden)                   */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2016                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.Common.Data.FilterString.Formatter
{
	#region usings

	using System;
	using System.Runtime.Serialization;
	using Zeiss.PiWeb.Api.Rest.Common.Data.FilterString.Tree;

	#endregion

	[Serializable]
	public class UnsupportedTokenException : FormaterException
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UnsupportedTokenException"/> class.
		/// </summary>
		public UnsupportedTokenException( Token unsupportedToken )
			: base( $"Token type '{unsupportedToken.Type}' is not supported by this formater." )
		{
			UnsupportedToken = unsupportedToken;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnsupportedTokenException" /> class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"></see> that holds the serialized object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"></see> that contains contextual information about the source or destination.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="info" /> parameter is <see langword="null" />.
		/// </exception>
		/// <exception cref="SerializationException">
		/// The class name is <see langword="null" /> or <see cref="Exception.HResult"></see> is zero (0).
		/// </exception>
		protected UnsupportedTokenException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
			foreach( var entry in info )
			{
				if( entry.Name == nameof( UnsupportedToken ) )
				{
					UnsupportedToken = (Token)entry.Value;
				}
			}
		}

		#endregion

		#region properties

		public Token UnsupportedToken { get; private set; }

		#endregion

		#region methods

		/// <inheritdoc />
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );
			info.AddValue( nameof( UnsupportedToken ), UnsupportedToken );
		}

		#endregion
	}
}