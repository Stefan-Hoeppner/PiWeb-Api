#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.HttpClient.ReportManagement
{

	#region usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Zeiss.PiWeb.Api.Rest.Common.Client;
	using Zeiss.PiWeb.Api.Rest.Contracts;
	using Zeiss.PiWeb.Api.Rest.Dtos;
	using Zeiss.PiWeb.Api.Rest.Dtos.ReportManagement;
	using Zeiss.PiWeb.Api.Rest.HttpClient.Builder;

	#endregion

	/// <summary>
	/// Client class for communicating with the REST based report management service.
	/// </summary>
	public sealed class ReportManagementServiceRestClient : CommonRestClientBase, IReportManagementServiceRestClient
	{
		#region constants

		/// <summary>
		/// The name of the endpoint of this service.
		/// </summary>
		public const string EndpointName = "ReportManagementServiceRest/";

		#endregion

		#region constructors

		/// <summary>
		/// Constructor. Instantiates a new <see cref="ReportManagementServiceRestClient"/> to communicate with the PiWeb-Server ReportManagementServiceRest.
		/// </summary>
		/// <param name="serverUri">The PiWeb Server uri, including port and instance</param>
		/// <param name="maxUriLength">The uri length limit</param>
		/// <param name="restClient">Custom implementation of RestClient</param>
		public ReportManagementServiceRestClient( [NotNull] Uri serverUri, int maxUriLength = RestClientBase.DefaultMaxUriLength, RestClientBase restClient = null )
			: base( restClient ?? new RestClient( serverUri, EndpointName, maxUriLength: maxUriLength, serializer: ObjectSerializer.SystemTextJson ) )
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportManagementServiceRestClient"/> class.
		/// </summary>
		/// <param name="settings">The settings of the rest service.</param>
		internal ReportManagementServiceRestClient( RestClientSettings settings )
			: base( new RestClient( EndpointName, settings ) )
		{ }

		#endregion

		#region interface IReportManagementServiceRestClient

		/// <inheritdoc />
		public ICustomRestClient CustomRestClient => _RestClient;

		/// <inheritdoc />
		public async Task<IEnumerable<ReportDirectoryDto>> GetReportDirectories( bool? deleted, bool? containsDeleted = null, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>();
			if( deleted != null )
				query.Add( ParameterDefinition.Create( "deleted", deleted.Value.ToString() ) );
			if( containsDeleted != null )
				query.Add( ParameterDefinition.Create( "containsDeleted", containsDeleted.Value.ToString() ) );

			return await _RestClient.Request<IEnumerable<ReportDirectoryDto>>( RequestBuilder.CreateGet( "ReportDirectories", query ), cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<IEnumerable<ReportDirectoryDto>> GetReportDirectories( Guid parentUuid, bool? deleted, bool? containsDeleted = null, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>
			{
				ParameterDefinition.Create( "parentUuid", parentUuid.ToString() )
			};
			if( deleted != null )
				query.Add( ParameterDefinition.Create( "deleted", deleted.Value.ToString() ) );
			if( containsDeleted != null )
				query.Add( ParameterDefinition.Create( "containsDeleted", containsDeleted.Value.ToString() ) );

			return await _RestClient.Request<IEnumerable<ReportDirectoryDto>>( RequestBuilder.CreateGet( "ReportDirectories", query ), cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<IEnumerable<ReportDirectoryDto>> GetReportDirectories( string parentPath, bool? deleted, bool? containsDeleted = null, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>
			{
				ParameterDefinition.Create( "parentPath", parentPath )
			};
			if( deleted != null )
				query.Add( ParameterDefinition.Create( "deleted", deleted.Value.ToString() ) );
			if( containsDeleted != null )
				query.Add( ParameterDefinition.Create( "containsDeleted", containsDeleted.Value.ToString() ) );

			return await _RestClient.Request<IEnumerable<ReportDirectoryDto>>( RequestBuilder.CreateGet( "ReportDirectories", query ), cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<ReportDirectoryDto> GetReportDirectory( Guid directoryUuid, CancellationToken cancellationToken = default )
		{
			return await _RestClient.Request<ReportDirectoryDto>(
				RequestBuilder.CreateGet( $"ReportDirectories/{directoryUuid}" ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task CreateReportDirectory( UpdateReportDirectoryDto reportDirectory, CancellationToken cancellationToken = default )
		{
			await _RestClient.Request(
				RequestBuilder.CreatePost( "ReportDirectories", Payload.Create( reportDirectory ) ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task UpdateReportDirectory( UpdateReportDirectoryDto reportDirectory, CancellationToken cancellationToken = default )
		{
			await _RestClient.Request(
				RequestBuilder.CreatePut( $"ReportDirectories/{reportDirectory.Uuid}", Payload.Create( reportDirectory ) ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task DeleteReportDirectory( Guid directoryUuid, bool permanently, CancellationToken cancellationToken = default )
		{
			try
			{
				await _RestClient.Request(
					RequestBuilder.CreateDelete( $"ReportDirectories/{directoryUuid}", ParameterDefinition.Create( "permanently", permanently.ToString() ) ),
					cancellationToken ).ConfigureAwait( false );
			}
			catch( WrappedServerErrorException )
			{
				// Due to the lack of hierarchy, deleting all elements in the recycle bin results in
				// already deleted elements being deleted again and causing an error.
			}
		}

		/// <inheritdoc />
		public async Task RestoreReportDirectory( Guid directoryUuid, CancellationToken cancellationToken = default )
		{
			await _RestClient.Request(
				RequestBuilder.CreatePost( $"ReportDirectories/{directoryUuid}", Payload.Empty ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public IAsyncEnumerable<ReportMetadataDto> GetReportMetadataListAsync( bool? deleted, CancellationToken cancellationToken = default )
		{
			return _RestClient.RequestEnumerated<ReportMetadataDto>(
				deleted == null
					? RequestBuilder.CreateGet( "Reports" )
					: RequestBuilder.CreateGet( "Reports", ParameterDefinition.Create( "deleted", deleted.Value.ToString() ) ),
				cancellationToken );
		}

		/// <inheritdoc />
		public IAsyncEnumerable<ReportMetadataDto> GetReportMetadataListAsync( Guid directoryUuid, bool? deleted, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>
			{
				ParameterDefinition.Create( "directoryUuid", directoryUuid.ToString() )
			};
			if( deleted != null )
				query.Add( ParameterDefinition.Create( "deleted", deleted.Value.ToString() ) );

			return _RestClient.RequestEnumerated<ReportMetadataDto>( RequestBuilder.CreateGet( "Reports", query ), cancellationToken );
		}

		/// <inheritdoc />
		public IAsyncEnumerable<ReportMetadataDto> GetReportMetadataListAsync( string directoryPath, bool? deleted, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>
			{
				ParameterDefinition.Create( "directoryPath", directoryPath )
			};
			if( deleted != null )
				query.Add( ParameterDefinition.Create( "deleted", deleted.Value.ToString() ) );

			return _RestClient.RequestEnumerated<ReportMetadataDto>( RequestBuilder.CreateGet( "Reports", query ), cancellationToken );
		}

		/// <inheritdoc />
		public async Task<ReportMetadataDto> GetReportMetadataAsync( Guid reportUuid, CancellationToken cancellationToken = default )
		{
			return await _RestClient.Request<ReportMetadataDto>(
				RequestBuilder.CreateGet( $"Reports/{reportUuid}" ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<byte[]> GetReportThumbnailAsync( Guid reportUuid, Guid? expectedMd5 = null, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>();

			if( expectedMd5.HasValue )
				query.Add( ParameterDefinition.Create( "expectedMd5", expectedMd5.Value.ToString() ) );

			return await _RestClient.RequestBytes(
				RequestBuilder.CreateGet( $"Reports/{reportUuid}/Thumbnail", query ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task UpdateReportMetadataAsync( Guid reportUuid, UpdateReportMetadataDto reportMetadata, CancellationToken cancellationToken = default )
		{
			await _RestClient.Request(
				RequestBuilder.CreatePut( $"Reports/{reportUuid}", Payload.Create( reportMetadata ) ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<Stream> GetReportContentAsync( Guid reportUuid, string entryPath = null, Guid? expectedMd5 = null, CancellationToken cancellationToken = default )
		{
			var query = new List<ParameterDefinition>();

			if( entryPath is not null )
				query.Add( ParameterDefinition.Create( "entryPath", entryPath ) );

			if( expectedMd5.HasValue )
				query.Add( ParameterDefinition.Create( "expectedMd5", expectedMd5.Value.ToString() ) );

			return await _RestClient.RequestStream(
				RequestBuilder.CreateGet( $"Reports/{reportUuid}/Content", query ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<Guid> CreateReportAsync( Stream report, string fileName, Guid? directoryUuid = null, CancellationToken cancellationToken = default )
		{
			var parameterDefinitions = directoryUuid == null
				? []
				: new[] { ParameterDefinition.Create( "directoryUuid", directoryUuid.ToString() ) };

			return await _RestClient.Request<Guid>(
				RequestBuilder.CreateWithAttachment( HttpMethod.Post, "Reports", report, "application/octet-stream", null, null, fileName, parameterDefinitions ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task UpdateReportAsync( Guid reportUuid, Stream report, string fileName, CancellationToken cancellationToken = default )
		{
			await _RestClient.Request(
				RequestBuilder.CreateWithAttachment( HttpMethod.Put, $"Reports/{reportUuid}/Content", report, "application/octet-stream", null, null, fileName ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task DeleteReportAsync( Guid reportUuid, bool permanently, CancellationToken cancellationToken = default )
		{
			try
			{
				await _RestClient.Request(
					RequestBuilder.CreateDelete( $"Reports/{reportUuid}", ParameterDefinition.Create( "permanently", permanently.ToString() ) ),
					cancellationToken ).ConfigureAwait( false );
			}
			catch( WrappedServerErrorException exception ) when( exception.StatusCode == HttpStatusCode.NotFound )
			{
				// Due to the lack of hierarchy, deleting all elements in the recycle bin results in
				// already deleted elements being deleted again and causing an error.
			}
		}

		/// <inheritdoc />
		public async Task RestoreReportAsync( Guid reportUuid, CancellationToken cancellationToken = default )
		{
			await _RestClient.Request(
				RequestBuilder.CreatePost( $"Reports/{reportUuid}", Payload.Empty ),
				cancellationToken ).ConfigureAwait( false );
		}

		/// <inheritdoc />
		public async Task<IEnumerable<ReportEntryMetadataDto>> GetReportEntriesAsync( Guid reportUuid, CancellationToken cancellationToken = default )
		{
			return await _RestClient.Request<IEnumerable<ReportEntryMetadataDto>>(
				RequestBuilder.CreateGet( $"Reports/{reportUuid}/Entries" ),
				cancellationToken ).ConfigureAwait( false );
		}

		#endregion
	}
}