#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.Contracts
{

	#region usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading;
	using System.Threading.Tasks;
	using Zeiss.PiWeb.Api.Rest.Dtos;
	using Zeiss.PiWeb.Api.Rest.Dtos.ReportManagement;

	#endregion

	/// <summary>
	/// Client class for communicating with the REST based report management service.
	/// </summary>
	public interface IReportManagementServiceRestClientBase<T> where T : ReportManagementServiceFeatureMatrix
	{
		#region properties

		/// <summary>
		/// A custom rest client that can be used to execute rest request created by a rest request builder.
		/// </summary>
		public ICustomRestClient CustomRestClient { get; }

		#endregion

		#region methods

		/// <summary>
		/// Retrieves all report directories.
		/// </summary>
		/// <param name="deleted">
		/// <see langword="true" /> if the result should be restricted to deleted report directories,
		/// <see langword="false" /> if the result should be restricted to non-deleted report directories,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="containsDeleted">
		/// <see langword="true" /> if the result should be restricted to report directories that contains deleted reports or sub-directories
		/// <see langword="false" /> if the result should be restricted to report directories that contains no deleted reports or sub-directories,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<IEnumerable<ReportDirectoryDto>> GetReportDirectories( bool? deleted, bool? containsDeleted = null, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves all report directories that are located in the specified directory.
		/// </summary>
		/// <param name="parentUuid">The UUID of the parent report directory.</param>
		/// <param name="deleted">
		/// <see langword="true" /> if the result should be restricted to deleted report directories,
		/// <see langword="false" /> if the result should be restricted to non-deleted report directories,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="containsDeleted">
		/// <see langword="true" /> if the result should be restricted to report directories that contains deleted reports or sub-directories
		/// <see langword="false" /> if the result should be restricted to report directories that contains no deleted reports or sub-directories,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<IEnumerable<ReportDirectoryDto>> GetReportDirectories( Guid parentUuid, bool? deleted, bool? containsDeleted = null, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves all report directories that are located in the specified directory.
		/// </summary>
		/// <param name="parentPath">The path of the parent report directory.</param>
		/// <param name="deleted">
		/// <see langword="true" /> if the result should be restricted to deleted report directories,
		/// <see langword="false" /> if the result should be restricted to non-deleted report directories,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="containsDeleted">
		/// <see langword="true" /> if the result should be restricted to report directories that contains deleted reports or sub-directories
		/// <see langword="false" /> if the result should be restricted to report directories that contains no deleted reports or sub-directories,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<IEnumerable<ReportDirectoryDto>> GetReportDirectories( string parentPath, bool? deleted, bool? containsDeleted = null, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves a report directory by a given UUID.
		/// </summary>
		/// <param name="directoryUuid">The UUID of the report directory.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<ReportDirectoryDto> GetReportDirectory( Guid directoryUuid, CancellationToken cancellationToken = default );

		/// <summary>
		/// Creates a report directory using a new UUID or the specified UUID if it exists and is not zero.
		/// </summary>
		/// <param name="reportDirectory">The new report directory.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task CreateReportDirectory( UpdateReportDirectoryDto reportDirectory, CancellationToken cancellationToken = default );

		/// <summary>
		/// Creates or updates a report directory by a given UUID.
		/// </summary>
		/// <param name="reportDirectory">The new report directory.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task UpdateReportDirectory( UpdateReportDirectoryDto reportDirectory, CancellationToken cancellationToken = default );

		/// <summary>
		///  Deletes a report directory by a given UUID.
		/// </summary>
		/// <param name="directoryUuid">The UUID of the report directory.</param>
		/// <param name="permanently">If set to <see langword="true"/> the report directory cannot be restored.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task DeleteReportDirectory( Guid directoryUuid, bool permanently, CancellationToken cancellationToken = default );

		/// <summary>
		///  Restores a deleted report directory by a given UUID.
		/// </summary>
		/// <param name="directoryUuid">The UUID of the report directory.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task RestoreReportDirectory( Guid directoryUuid, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves a list of all reports.
		/// </summary>
		/// <param name="deleted">
		/// <see langword="true" /> if the result should be restricted to deleted reports,
		/// <see langword="false" /> if the result should be restricted to non-deleted reports,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		IAsyncEnumerable<ReportMetadataDto> GetReportMetadataListAsync( bool? deleted, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves a list of all reports that are located in the specified directory.
		/// </summary>
		/// <param name="directoryUuid">Identifies the report directory.</param>
		/// <param name="deleted">
		/// <see langword="true" /> if the result should be restricted to deleted reports,
		/// <see langword="false" /> if the result should be restricted to non-deleted reports,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		IAsyncEnumerable<ReportMetadataDto> GetReportMetadataListAsync( Guid directoryUuid, bool? deleted, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves a list of all reports that are located in the specified directory.
		/// </summary>
		/// <param name="directoryPath">Identifies the report directory by a path.</param>
		/// <param name="deleted">
		/// <see langword="true" /> if the result should be restricted to deleted reports,
		/// <see langword="false" /> if the result should be restricted to non-deleted reports,
		/// <see langword="null" /> if the result should not be restricted regarding deletion.
		/// </param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		IAsyncEnumerable<ReportMetadataDto> GetReportMetadataListAsync( string directoryPath, bool? deleted, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves a report by a given UUID.
		/// </summary>
		/// <param name="reportUuid">Identifies the <see cref="ReportMetadataDto"/> to be retrieved.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<ReportMetadataDto> GetReportMetadataAsync( Guid reportUuid, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves a report thumbnail by a given report UUID.
		/// </summary>
		/// <param name="reportUuid">Identifies the report thumbnail to be retrieved.</param>
		/// <param name="expectedMd5">The MD5 checksum of the report that the client expects. If specified, client caching can benefit from it.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<byte[]> GetReportThumbnailAsync( Guid reportUuid, Guid? expectedMd5 = null, CancellationToken cancellationToken = default );

		/// <summary>
		/// Updates report metadata using given <see cref="ReportMetadataDto"/>.
		/// </summary>
		/// <param name="reportUuid">Identifies the report to be updated.</param>
		/// <param name="reportMetadata">The new metadata.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task UpdateReportMetadataAsync( Guid reportUuid, UpdateReportMetadataDto reportMetadata, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves the content of a report by a given UUID.
		/// </summary>
		/// <param name="reportUuid">Identifies the report to be retrieved.</param>
		/// <param name="entryPath">The path of the entry inside the report.</param>
		/// <param name="expectedMd5">The MD5 checksum of the report that the client expects. If specified, client caching can benefit from it.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<Stream> GetReportContentAsync( Guid reportUuid, string entryPath = null, Guid? expectedMd5 = null, CancellationToken cancellationToken = default );

		/// <summary>
		/// Creates a report using a new UUID.
		/// </summary>
		/// <param name="report">The data to create a new report from.</param>
		/// <param name="fileName">Original file name of the report.</param>
		/// <param name="directoryUuid">The directory where the report should be placed. If omitted, the root directory is used.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<Guid> CreateReportAsync( Stream report, string fileName, Guid? directoryUuid = null, CancellationToken cancellationToken = default );

		/// <summary>
		/// Creates or updates a report by a given UUID.
		/// </summary>
		/// <param name="reportUuid">Identifies the report to be retrieved.</param>
		/// <param name="report">The data to update an existing report from.</param>
		/// <param name="fileName">Original file name of the report.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task UpdateReportAsync( Guid reportUuid, Stream report, string fileName, CancellationToken cancellationToken = default );

		/// <summary>
		/// Deletes a report by a given UUID.
		/// </summary>
		/// <param name="reportUuid">Identifies the report to be deleted.</param>
		/// <param name="permanently">If set to <see langword="true"/> the report cannot be restored.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task DeleteReportAsync( Guid reportUuid, bool permanently, CancellationToken cancellationToken = default );

		/// <summary>
		/// Restores a deleted report by a given UUID.
		/// </summary>
		/// <param name="reportUuid">Identifies the report to be restored.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task RestoreReportAsync( Guid reportUuid, CancellationToken cancellationToken = default );

		/// <summary>
		/// Retrieves all report entries of the specified report.
		/// </summary>
		/// <param name="reportUuid">The UUID of the report.</param>
		/// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
		Task<IEnumerable<ReportEntryMetadataDto>> GetReportEntriesAsync( Guid reportUuid, CancellationToken cancellationToken = default );

		#endregion
	}
}