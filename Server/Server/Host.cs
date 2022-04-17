using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace Server
{
    public static class Host
    {
        // Private Fields:
        private static bool _shouldExit = false;
        private static ManualResetEvent _shouldExitWaitHandle;
        private static HttpListener _listener;

        // Public Methods:
        public static void Start()
        {
            // Init server:
            _shouldExitWaitHandle = new ManualResetEvent(_shouldExit);
            _listener = new HttpListener();

            // Cancel server:
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
                _shouldExit = true;
                _shouldExitWaitHandle.Set();
            };

            // Set listner port and begin listening:
            _listener.Prefixes.Add("http://localhost:8080/");
            _listener.Start();
            Console.WriteLine("Server listening at port 8080");

            while (!_shouldExit)
            {
                // Every request to the http server will result in a new HttpContext:
                IAsyncResult contextAsyncResult = _listener.BeginGetContext(HandleClientRequest, null);

                // Wait for the program to exit or for a new request:
                WaitHandle.WaitAny(new WaitHandle[] { contextAsyncResult.AsyncWaitHandle, _shouldExitWaitHandle });
            }

            // Stop server:
            _listener.Stop();
            Console.WriteLine("Server stopped");
        }

        // Private Methods:
        private static void HandleClientRequest(IAsyncResult asyncResult)
        {
            var context = _listener.EndGetContext(asyncResult);
            context.Response.SetResponseOutput(RouteRequest(context.Request));
        }

        private static string RouteRequest(HttpListenerRequest request)
        {
            string[] routing = request.RawUrl.Split('/');

            // Switch case for the request type:
            switch (routing[1])
            {
                case "jobs": // Request for jobs:
                    return ProduceJobsJson(int.Parse(routing[2]));

                case "jobTitles": // Request for job titles:
                    return JsonConvert.SerializeObject(AutoCompleteJobTitles(routing[2]), Formatting.None);

                default:
                    return string.Empty;
            }
        }

        private static string ProduceJobsJson(int id)
        {
            JobTitle jobTitle = DatabaseHandler.ReadJobTitels().First(title => title.ID == id);
            List<Job> jobs = DatabaseHandler.ReadJobs(jobTitle);

            string jobTitleJson = JsonConvert.SerializeObject(jobTitle, Formatting.None); 
            string jobsJson = JsonConvert.SerializeObject(jobs.ToArray(), Formatting.None);

            // Concatenate json string:
            return "{" + $"\"JobTitle\":{jobTitleJson}, \"Jobs\":{jobsJson}" + "}";
        }

        private static JobTitle[] AutoCompleteJobTitles(string text)
        {
            if (text.Length <= 1)
                return new JobTitle[0];

            return DatabaseHandler.ReadJobTitels()
                   .Where(item => item.Name.ToLower().StartsWith(text.ToLower()))
                   .ToArray();
        }

    }
}
