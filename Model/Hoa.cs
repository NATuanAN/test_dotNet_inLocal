using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace aspTest.Models
{
    public class MongoDbSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }

    }
}

public class Hoa
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("mahoa")]
    public string MaHoa { get; set; } = null!;

    [BsonElement("maloai")]
    public string MaLoai { get; set; } = null!;

    [BsonElement("tenhoa")]
    public string TenHoa { get; set; } = null!;

    [BsonElement("giaban")]
    public string GiaBan { get; set; } = null!;

    [BsonElement("hinh")]
    public string Hinh { get; set; } = null!;

    [BsonElement("mota")]
    public string MoTa { get; set; } = null!;
}
