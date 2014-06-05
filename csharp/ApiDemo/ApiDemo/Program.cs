using System;
using VersionOne.SDK.APIClient;

namespace ApiDemo
{
	class Program
	{
		const string BASE_URL = "https://www14.v1host.com/v1sdktesting";
        	private const string SecretsFile = @"C:\Users\mheffel\Documents\GitHub\api-examples\csharp\ApiDemo\client_secrets.json";
        	private const string CredsFile = @"C:\Users\mheffel\Documents\GitHub\api-examples\csharp\ApiDemo\stored_credentials.json";
	
		static void Main(string[] args)
		{
            		var dataConnector = new VersionOneAPIConnector(BASE_URL + "/rest-1.v1/").WithOAuth2(SecretsFile, CredsFile);
			var metaConnector = new VersionOneAPIConnector(BASE_URL + "/meta.v1/");
				
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
			Console.ReadLine();
		}
	}
}
