using MongoGogo.Connection;

namespace Campus_System_Database_Model
{
    public class CampusSystemMongoContext : GoContext<CampusSystemMongoContext>
    {
        [MongoDatabase("data")]
        public class Data { }

        public CampusSystemMongoContext(string connectionString) : base(connectionString)
        {
        }
    }
}
