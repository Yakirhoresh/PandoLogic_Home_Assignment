using System.IO;
using System.Net;

namespace Server
{
    public static class HttpServices
    {
        public static string GetRequestBody(this HttpListenerRequest request)
        {
            // Read request stream into a string:
            if (request.HasEntityBody)
                using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    return reader.ReadToEnd();
            return null;
        }

        public static void SetResponseOutput(this HttpListenerResponse response, string output)
        {
            // Set necessary heading:
            response.ContentType = "application/json";
            response.AddHeader("Access-Control-Allow-Origin", "*");

            // Write output object to response stream:
            using (StreamWriter writer = new StreamWriter(response.OutputStream))
                writer.Write(output);
        }
    }
}