using System.Collections.Generic;

namespace PodCastAppFormVersion
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public List<string> friends{get; set; }
    }

    class UserIDMap
    {
        public string Name { get; set; }

        public List<string> ID { get; set; }
    }
}