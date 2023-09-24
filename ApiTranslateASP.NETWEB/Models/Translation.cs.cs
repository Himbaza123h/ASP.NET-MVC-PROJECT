using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiTranslateASP.NETWEB.Models
{
    public class Translation
    {
        [BsonId]
        public int Id { get; set; }  
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
    }
}