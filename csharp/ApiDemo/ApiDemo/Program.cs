using System;
using System.IO;
using System.Net;
using VersionOne.SDK.APIClient;

namespace ApiDemo
{
	class Program
	{
		//private const string BASE_URL = "https://www7.v1host.com/V1SSOTest";
		//private const string BASE_URL = "https://www7.v1host.com/V1Production";
		//private const string SecretsFile = @"C:\Users\JKoberg\support\ssotest\client_secrets.json";
		//private const string CredsFile = @"C:\Users\JKoberg\support\ssotest\stored_credentials.json";


		private const string BASE_URL = "http://jkoberg1/VersionOne.Web";
		private const string SecretsFile = @"C:\Users\JKoberg\support\ssotest\localhost\client_secrets.json";
		private const string CredsFile = @"C:\Users\JKoberg\support\ssotest\localhost\stored_credentials.json";


//		private const string BASE_URL = "http://jkoberg1/VersionOne_13_2_6_73";
//		private const string SecretsFile = @"C:\Users\JKoberg\support\ssotest\local_13_2_6_73\client_secrets.json";
//		private const string CredsFile = @"C:\Users\JKoberg\support\ssotest\local_13_2_6_73\stored_credentials.json";
//


		//private const string BASE_URL = "http://jkoberg1/VersionOne_SSOTest";
		//private const string SecretsFile = @"C:\Users\JKoberg\support\ssotest\local_sso\client_secrets.json";
		//private const string CredsFile = @"C:\Users\JKoberg\support\ssotest\local_sso\stored_credentials.json";



		static void Main(string[] args)
		{

            var url = args.Length >= 1 ? args[1] : BASE_URL;

			var storage = new OAuth2Client.Storage.JsonFileStorage(
                args.Length >= 2 ? args[1] : SecretsFile,
                args.Length >= 3 ? args[2] : CredsFile
                );
			
			var dataConnector = new V1OAuth2APIConnector(BASE_URL + "/rest-1.oauth.v1/", storage);
			var metaConnector = new V1OAuth2APIConnector(BASE_URL + "/meta.v1/", storage);

            var metaModel = new MetaModel(metaConnector);
            var services = new Services(metaModel, dataConnector);

            var scopeType = metaModel.GetAssetType("Member");
            var nameAttr = scopeType.GetAttributeDefinition("Name");
            var descAttr = scopeType.GetAttributeDefinition("Nickname");
            var worksItemsNameAttr = scopeType.GetAttributeDefinition("OwnedWorkitems.Name");

            var query = new Query(scopeType);
            var whereAdmin = new FilterTerm(descAttr);
            whereAdmin.Equal("admin");
            var whereNotTheAdmin = new FilterTerm(nameAttr);
            whereNotTheAdmin.NotEqual("theAdmin");
            var andFilter = new AndFilterTerm(whereAdmin, whereNotTheAdmin);
            query.Filter = andFilter;
            query.Selection.AddRange(new[] { nameAttr, descAttr, worksItemsNameAttr });
			try
			{
				var result = services.Retrieve(query);

				foreach (var asset in result.Assets)
				{
					Console.WriteLine("Name: " + asset.GetAttribute(nameAttr).Value);
					Console.WriteLine("Description: " + asset.GetAttribute(descAttr).Value);
					var workItems = asset.GetAttribute(worksItemsNameAttr).ValuesList;
					Console.WriteLine("Workitems count: " + workItems.Count);
					foreach (var workitem in workItems)
					{
						Console.WriteLine("Workitem: " + workitem);
					}
				}
			}
			catch (WebException ex)
			{
				var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
				System.Console.Write(resp);
			}

			Console.ReadLine();
		}
	}
}
