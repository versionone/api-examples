using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nito.AsyncEx.Synchronous;

namespace CapOneQueryExample
{
  internal class Program
  {
    /// <summary>
    /// Command line Main().
    /// Just calls MainAsync() with Nito.AsyncEx Task.WaitAndUnwrapException()
    /// so we properly run the code in the background and get proper error notifications.
    /// </summary>
    /// <param name="args">Arguments from the command line</param>
    private static void Main(string[] args)
    {
      var mainTask = MainAsync(args);
      mainTask.WaitAndUnwrapException();
    }

    /// <summary>
    /// Async program entry point.
    /// We use async here because we need to use the Microsoft System.Net.Http library,
    /// which only exports async methods.
    /// </summary>
    /// <param name="args">Unused, but left in to match the signature of Main()</param>
    /// <returns>A Task that is already running the report.</returns>
    private static async Task MainAsync(string[] args)
    {
      var url = "https://www14.v1host.com/v1sdktesting/query.legacy.v1";
      var queryBody = ThreeIterationReport.Query;
      var results = await GetResults<ThreeIterationReport.StoryDto>(url, queryBody);
      var stories = results[0];
      foreach (var storyDto in stories)
          Console.WriteLine(storyDto);
    }

    static async Task<List<List<T>>> GetResults<T>(string url, string queryBody, string username="admin", string password="admin")
    {
        using (var handler = new HttpClientHandler {Credentials = new NetworkCredential("username", "password")})
        {
            using (var client = new HttpClient(handler))
            {
                var httpResp = await client.PostAsync(url, new StringContent(queryBody));
                var httpBody = await httpResp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<List<T>>>(httpBody);
            }
        }
    }
  }
}