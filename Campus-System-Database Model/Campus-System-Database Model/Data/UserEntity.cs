using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoGogo.Connection;

namespace Campus_System_Database_Model.Data
{
    /// <summary>
    /// 使用者帳號
    /// </summary>
    [MongoCollection(typeof(CampusSystemMongoContext.Data), "User")]
    public class UserEntity
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("telephone")]
        public string? Telephone { get; set; }

        [BsonElement("role")]
        [BsonRepresentation(BsonType.String)]
        public UserRole Role { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public DocStatus Status { get; set; }

        [BsonElement("create_time")]
        public DateTime CreateTime { get; set; }

        [BsonElement("update_time")]
        public DateTime? UpdateTime { get; set; }
    }

    public enum UserRole
    {
        creator,
        admin,
        manager,
        teacher,
        student
    }
}
