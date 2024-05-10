#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Api.Rest.Tests.HttpClient.ReportManagement
{
	#region usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;
	using Zeiss.PiWeb.Api.Rest.Dtos.ReportManagement;
	using Zeiss.PiWeb.Api.Rest.HttpClient.ReportManagement;

	#endregion

	[TestFixture]
	public class ReportManagementServiceRestClientTest
	{
		#region constants

		private const int Port = 8081;

		#endregion

		#region members

		private static readonly Uri Uri = new Uri( $"http://localhost:{Port}/" );

		#endregion

		#region methods

		[Test]
		[TestCase(true)]
		[TestCase(false)]
		[TestCase(null)]
		public async Task GetReportMetadataListAsync_NoMetadata_ReturnEmptyEnumerable(bool?  deleted)
		{
			using var webServer = WebServer.StartNew( Port );
			RegisterReportManagementReportMetadataResponse( webServer, deleted, [] );

			using var sut = new ReportManagementServiceRestClient( Uri );
			var actual = sut.GetReportMetadataListAsync( deleted );

			var count = 0;
			await foreach( var elem in actual )
			{
				count++;
			}

			count.Should().Be( 0 );
		}

		[Test]
		[TestCase(true)]
		[TestCase(false)]
		[TestCase(null)]
		public async Task GetReportMetadataListAsync_MetaDataAvailable_ReturnsCorrectNumberOfDtos(bool? deleted)
		{
			using var webServer = WebServer.StartNew( Port );
			RegisterReportManagementReportMetadataResponse( webServer, deleted, [Stubs.ReportMetadataDto.Create(), Stubs.ReportMetadataDto.Create()] );

			using var sut = new ReportManagementServiceRestClient( Uri );
			var actual = sut.GetReportMetadataListAsync( deleted);

			var count = 0;
			await foreach( var elem in actual )
			{
				count++;
			}

			count.Should().Be( 2 );
		}

		[Test]
		[TestCase(true)]
		[TestCase(false)]
		[TestCase(null)]
		public async Task GetReportMetadataListAsync_MetaDataAvailable_ReturnsCorrectMetadata(bool? deleted)
		{
			using var webServer = WebServer.StartNew( Port );
			var availableMetadata = new ReportMetadataDto
			{
				Uuid = Guid.NewGuid(),
				DirectoryUuid = Guid.NewGuid(),
				FileName = "Some File Name",
				Creator = Guid.NewGuid(),
				Created = DateTime.Now,
				LastModifier = Guid.NewGuid(),
				LastModified = DateTime.Now,
				Deleter = null,
				Deleted = null,
				Size = 1337,
				MD5 = Guid.NewGuid(),
				Version = new Version( 8, 3, 5 ),
				DisplayName = "Test Report",
				Description = "This report exists for testing purposes.",
				ReportGroup = "no group",
				ReportCreator = "author",
				ReportCreated = DateTime.Now,
				ReportLastModifier = "no one",
				ReportLastModified = DateTime.Now,
				Links = new ReportMetadataLinksDto
				{
					Content = new LinkDto { Href = "ftp://content.com" },
					Self = new LinkDto { Href = "https://self.com" },
					Thumbnail = new LinkDto { Href = "https://self.com/thumbnail" }
				}
			};
			RegisterReportManagementReportMetadataResponse( webServer, deleted, [availableMetadata] );

			using var sut = new ReportManagementServiceRestClient( Uri );
			var result = sut.GetReportMetadataListAsync( deleted);

			ReportMetadataDto actual = null;
			await foreach( var elem in result )
			{
				actual = elem;
				break;
			}

			JsonConvert.SerializeObject( availableMetadata ).Should().BeEquivalentTo( JsonConvert.SerializeObject( actual ) );
		}

		private static void RegisterReportManagementReportMetadataResponse( WebServer webServer, bool? deleted , ICollection<ReportMetadataDto> reports )
		{
			var url = $"/ReportManagementServiceRest/Reports{(deleted != null ? $"?deleted={deleted}" : "")}";
			var responseJson = JsonConvert.SerializeObject( reports );
			webServer.RegisterResponse( url, responseJson );
		}

		#endregion
	}

	internal static class Stubs
	{
		public static class ReportMetadataDto
		{
			internal static Zeiss.PiWeb.Api.Rest.Dtos.ReportManagement.ReportMetadataDto Create()
			{
				return new Rest.Dtos.ReportManagement.ReportMetadataDto
				{
					Uuid = Guid.NewGuid(),
					DirectoryUuid = Guid.NewGuid(),
					FileName = "[FileName]",
					Creator = Guid.NewGuid(),
					Created = DateTime.Now,
					LastModifier = Guid.NewGuid(),
					LastModified = DateTime.Now,
					Deleter = null,
					Deleted = null,
					Size = 100,
					MD5 = Guid.NewGuid(),
					Version = new Version(0,0,0),
					DisplayName = "[DisplayName]",
					Description = "[Description]",
					ReportGroup = "[ReporterGroup]",
					ReportCreator = "[ReportCreator]",
					ReportCreated = DateTime.Now,
					ReportLastModifier = "[ReportLastModifier]",
					ReportLastModified = DateTime.Now,
					Links = new ReportMetadataLinksDto
					{
						Content = new LinkDto {Href = "[Content]"},
						Self = new LinkDto {Href = "[Self]"},
						Thumbnail = new LinkDto {Href = "[Thumbnail]"}
					}
				};
			}
		}
	}
}