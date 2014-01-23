using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nito.AsyncEx.Synchronous;
using OAuth2Client;

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
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
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
            var results = await GetResults<ThreeIterationQuery.TimeboxDto>();
            var stories = results[0];
            foreach (var storyDto in stories) Console.WriteLine(storyDto);
        }

        static async Task<List<List<T>>> GetResults<T>()
        {
            var queryBody = new StringContent(ThreeIterationQuery.Query);

            using (var handler = new AuthHandler.OAuth2BearerHandler(
                new HttpClientHandler(),
                new Storage.JsonFileStorage(
                    @"C:\Users\JKoberg\src\CapOneQueryExample\CapOneQueryExample\bin\Debug\client_secrets.json",
                    @"C:\Users\JKoberg\src\CapOneQueryExample\CapOneQueryExample\bin\Debug\stored_credentials.json"),
                "query-api-1.0",
                null))

            using (var client = new HttpClient(handler))
            {
                var httpResp = await client.PostAsync("https://www1.v1host.com/Test/query.v1", queryBody);
                var httpBody = await httpResp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<List<T>>>(httpBody);
            }
        }
    }
}