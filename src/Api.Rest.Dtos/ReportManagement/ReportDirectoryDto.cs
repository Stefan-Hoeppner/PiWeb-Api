#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.Dtos.ReportManagement
{

	#region usings

	using System;
	using System.Text.Json.Serialization;

	#endregion

	/// <summary>
	/// Contains the report directory.
	/// </summary>
	public class ReportDirectoryDto
	{
		#region properties

		/// <summary>
		/// Gets the unique identifier of the report directory.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "uuid" )]
		[JsonPropertyName( "uuid" )]
		public Guid Uuid { get; set; }

		/// <summary>
		/// Gets the path and name of the report directory.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "path" )]
		[JsonPropertyName( "path" )]
		public string Path { get; set; }

		/// <summary>
		/// Gets the unique identifier of the parent report directory or <see langword="null"/> if it is the root directory.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "parentUuid" )]
		[JsonPropertyName( "parentUuid" )]
		public Guid? ParentUuid { get; set; }

		/// <summary>
		/// Gets the user UUID of the user who created the directory.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "creator" )]
		[JsonPropertyName( "creator" )]
		public Guid Creator { get; set; }

		/// <summary>
		/// Gets the creation time (UTC) of the directory.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "created" )]
		[JsonPropertyName( "created" )]
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets the user UUID of the user who renamed or moved the directory the last time.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "lastModifier" )]
		[JsonPropertyName( "lastModifier" )]
		public Guid LastModifier { get; set; }

		/// <summary>
		/// Gets the last time (UTC) when the directory was renamed or moved.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "lastModified" )]
		[JsonPropertyName( "lastModified" )]
		public DateTime LastModified { get; set; }

		/// <summary>
		/// Gets the user UUID of the user who deleted the directory or <see langword="null"/> if the directory is not deleted.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "deleter" )]
		[JsonPropertyName( "deleter" )]
		public Guid? Deleter { get; set; }

		/// <summary>
		/// Gets the deletion time (UTC) of the report directory or <see langword="null"/> if the directory is not deleted.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "deleted" )]
		[JsonPropertyName( "deleted" )]
		public DateTime? Deleted { get; set; }

		#endregion
	}
}