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
	/// Contains the report directory for updating.
	/// </summary>
	public class UpdateReportDirectoryDto
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

		#endregion
	}
}