using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace FilmsApp.Models
{
    public class Film
    {
                [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id {get; set;}

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string FilmName {get; set;}

        public string Director {get; set;}

        public string Hero {get; set;}

        public string Heroine {get; set;}

        public int Rating {get; set;}
    }
}