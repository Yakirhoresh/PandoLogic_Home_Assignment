using System.Collections.Specialized;

namespace Server
{
    public class Job
    {
        // Public Properties:
        // public int ID { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        // Constructor:
        public Job(NameValueCollection nvc)
        {
            // ID = int.Parse(nvc["JobId"]);
            City = nvc["City"];
            State = nvc["State"];
        }
    }
}
