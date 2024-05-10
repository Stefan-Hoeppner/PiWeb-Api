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
	/// Contains the report entry metadata.
	/// </summary>
	public class ReportEntryMetadataDto
	{
		#region properties

		/// <summary>
		/// Gets the path of the entry.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "path" )]
		[JsonPropertyName( "path" )]
		public string Path { get; set; }

		/// <summary>
		/// Gets the size of the entry in bytes.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "size" )]
		[JsonPropertyName( "size" )]
		public long Size { get; set; }

		/// <summary>
		/// Gets the time of the last modification.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "lastModified" )]
		[JsonPropertyName( "lastModified" )]
		public DateTime LastModified { get; set; }

		#endregion
	}
}