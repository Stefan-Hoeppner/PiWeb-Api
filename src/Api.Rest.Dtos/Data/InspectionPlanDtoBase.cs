﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2015                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.Dtos.Data
{
	#region usings

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;
	using Zeiss.PiWeb.Api.Core;
	using Zeiss.PiWeb.Api.Rest.Dtos.Converter;
	using Zeiss.PiWeb.Api.Rest.Dtos.JsonConverters;
	using Attribute = Zeiss.PiWeb.Api.Core.Attribute;

	#endregion

	/// <summary>
	/// This is the base class for inspection plan entities (i.e. parts and characteristics). Each inspection plan entity is identified by an <see cref="Uuid"/>
	/// (constants, even when renamed) and a unique <see cref="Path"/>.
	/// </summary>
	[DebuggerDisplay( "{" + nameof( Path ) + "}" )]
	public abstract class InspectionPlanDtoBase : IAttributeItem
	{
		#region members

		private IReadOnlyList<Attribute> _Attributes;

		[Newtonsoft.Json.JsonProperty( "path" ), Newtonsoft.Json.JsonConverter( typeof( PathInformationConverter ) )]
		private PathInformation _Path;

		#endregion

		#region properties

		/// <summary>
		/// Indexer for accessing the attribute value with the specified <code>key</code>.
		/// </summary>
		public string this[ ushort key ]
		{
			get => this.GetAttributeValue( key );
			set => this.SetAttributeValue( key, value );
		}

		/// <summary>
		/// Gets or sets the uuid of this inspection plan entity. The uuid is always constant, even if
		/// this entity is renamed.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "uuid" )]
		[JsonPropertyName( "uuid" )]
		public Guid Uuid { get; set; }

		/// <summary>
		/// Gets or sets the comment of this inspection plan entity.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "comment" )]
		[JsonPropertyName( "comment" )]
		public string Comment { get; set; }

		/// <summary>
		/// Gets or sets the path of this inspection plan entity.
		/// </summary>
		[Newtonsoft.Json.JsonIgnore]
		[JsonPropertyName( "path" ), JsonConverter( typeof( JsonPathInformationConverter ) )]
		public PathInformation Path
		{
			[NotNull] get => _Path ?? PathInformation.Root;
			set => _Path = value;
		}

		/// <summary>
		/// This is the version number of this entity. The version number is using a global versioning scheme accross all
		/// version changes of the whole inspection plan. This means, that single instances of a part or characteristic can
		/// have non consecutive version numbers.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "version" )]
		[JsonPropertyName( "version" )]
		public uint Version { get; set; }

		/// <summary>
		/// Contains the date and time of the last change applied to this instance.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "timestamp" )]
		[JsonPropertyName( "timestamp" )]
		public DateTime Timestamp { get; set; }

		#endregion

		#region methods

		/// <inheritdoc />
		public override string ToString()
		{
			return Version + " - " + Path;
		}

		#endregion

		#region interface IAttributeItemDto

		/// <inheritdoc />
		[Newtonsoft.Json.JsonProperty( "attributes" ), Newtonsoft.Json.JsonConverter( typeof( AttributeArrayConverter ) )]
		[JsonPropertyName( "attributes" ), JsonConverter( typeof( JsonAttributeArrayConverter ) )]
		public IReadOnlyList<Attribute> Attributes
		{
			[NotNull] get => _Attributes ?? Array.Empty<Attribute>();
			set => _Attributes = value;
		}

		#endregion
	}
}
