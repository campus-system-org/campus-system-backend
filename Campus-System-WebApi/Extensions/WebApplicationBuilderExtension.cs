using Campus_System_Database_Model;
using MongoDB.Bson.Serialization;

namespace Campus_System_WebApi.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static IServiceCollection Add_MongoDb_Context(this IServiceCollection service, 
                                                             IConfiguration configuration)
        {
            var connString = configuration["mongo"]!;
            service.AddMongoContext(new CampusSystemMongoContext(connString));

            //add datetime serializer
            BsonSerializer.RegisterSerializer(typeof(DateTime), new TaiwanDateTimeSerializer());

            return service;
        }
    }
}
