using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx.Synchronous;
using OAuth2Client;
using Microsoft.FSharp.Core;

namespace VersionOne.Example.ItemsOpenInProject
{
	internal class Program
	{
		public const string ApiEndpoint = "https://www14.v1host.com/v1sdktesting/query.v1";

		public const string ApiQuery = @"
from: PrimaryWorkitem
where:
  Scope.Name: ""Sample: Release 1.0""
  Scope.AssetState: Active
select:
  - AssetType
  - Name
  - Estimate
";

		private static async Task DoRequest()
		{
			Console.WriteLine(ApiQuery);
			var storage = new FSharpOption<IStorageAsync>(new Storage.JsonFileStorage(@"..\..\client_secrets.json", @"..\..\stored_credentials.json"));
			var httpclient = OAuth2Client.HttpClientFactory.WithOAuth2("query-api-1.0", storage, null, null);

			var response = await httpclient.PostAsync(ApiEndpoint, new StringContent(ApiQuery));
			if (response.StatusCode != HttpStatusCode.OK)
			{
				var errorBody = await response.Content.ReadAsStringAsync();
				throw new Exception("Request failed:\n" + errorBody);
			}

			var jsonBody = await response.Content.ReadAsStringAsync();
			Console.WriteLine(jsonBody);
			var resultSets = JArray.Parse(jsonBody);
			var results =
				from item in resultSets[0]
				select new {Name = item["Name"], Estimate = item["Estimate"]}
				;

			foreach (var item in results)
			{
				Console.WriteLine("{0} {1}", item.Name, item.Estimate);
			}
		}

		private static void Main(string[] args)
		{
			DoRequest().WaitAndUnwrapException();
		}
	}
}
