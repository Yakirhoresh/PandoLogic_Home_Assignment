using System.Collections.Specialized;

namespace Server
{
    public class JobTitle
    {
        // Public Properties:
        public int ID { get; set; }
        public string Name { get; set; }

        // Constructor:
        public JobTitle()
        { }

        public JobTitle(NameValueCollection nvc)
        {
            ID = int.Parse(nvc["JobTitleId"]);
            Name = nvc["JobTitleName"];
        }
    }
}
