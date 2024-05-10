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
	/// Contains the report metadata for updating.
	/// </summary>
	public class UpdateReportMetadataDto
	{
		#region properties

		/// <summary>
		/// Gets the unique identifier of the report directory or <see langword="null"/> if the report should not be moved.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "directoryUuid" )]
		[JsonPropertyName( "directoryUuid" )]
		public Guid? DirectoryUuid { get; set; }

		/// <summary>
		/// Gets the file name of the report or <see langword="null"/> if the file name should not be updated.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "fileName" )]
		[JsonPropertyName( "fileName" )]
		public string FileName { get; set; }

		/// <summary>
		/// Gets the display name of the report or <see langword="null"/> if the display name should not be updated.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "displayName" )]
		[JsonPropertyName( "displayName" )]
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets the group of the report or <see langword="null"/> if the group should not be updated.
		/// </summary>
		[Newtonsoft.Json.JsonProperty( "reportGroup" )]
		[JsonPropertyName( "reportGroup" )]
		public string ReportGroup { get; set; }

		#endregion
	}
}