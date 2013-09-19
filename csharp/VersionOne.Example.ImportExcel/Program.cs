using System;
using System.Collections.Generic;
using System.Linq;

using VersionOne.SDK.ObjectModel.Filters; 
using VersionOne.SDK.ObjectModel;

namespace VersionOne.Example.ImportExcel
{
	public class MyExcelDataSource
	{
		public class MyExcelRow
		{
			public string StoryNumber { get; set; }
			public int NewEstimate { get; set; }
		}

		/// <summary>
		/// This is a sample implementation that should read your excel file.
		/// I don't seem to have the Excel Interop DLLs on my machine, so I just stubbed it for now.
		/// </summary>
		public static IEnumerable<MyExcelRow> GetMyExcelData()
		{
			return new[]
				{
					new MyExcelRow {StoryNumber = "B-01017", NewEstimate = 6},
					new MyExcelRow {StoryNumber = "B-01019", NewEstimate = 3}
				};
		}
	}



	class Program
	{
		const string Url = "https://www14.v1host.com/v1sdktesting";
		const string Username = "admin";
		const string Password = "admin";

		static void Main(string[] args)
		{
			var v1 = new V1Instance(Url, Username, Password);

			foreach (var row in MyExcelDataSource.GetMyExcelData())
			{
				var whereTerm = new KeyValuePair<string, string>("Number", row.StoryNumber);
				var whichStories = new StoryFilter();
				whichStories.ArbitraryWhereTerms.Add(whereTerm);
				foreach (var story in v1.Get.Stories(whichStories).Take(1))
				{
					Console.WriteLine("Got story {0}, updating...", story);
					story.Estimate = row.NewEstimate;
					story.Save("Updated from excel sheet on " + DateTimeOffset.Now);
					Console.WriteLine("Story updated.");
				}
			}
		}
	}
}
