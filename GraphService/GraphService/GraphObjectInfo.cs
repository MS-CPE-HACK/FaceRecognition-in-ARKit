using Newtonsoft.Json;

namespace GraphService
{
    public class GraphObjectInfo
    {
        [JsonProperty(PropertyName = "objectId")]
        public string ObjectId { get; set; }
        [JsonProperty(PropertyName = "givenName")]
        public string GivenName { get; set; }
        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }
        //[JsonProperty(PropertyName = "extension_18e31482d3fb4a8ea958aa96b662f508_BuildingName")]
        //public string BuildingName { get; set; }
        [JsonProperty(PropertyName = "extension_18e31482d3fb4a8ea958aa96b662f508_ReportsToEmailName")]
        public string ReportsToEmailName { get; set; }
        [JsonProperty(PropertyName = "department")]
        public string Department { get; set; }
        [JsonProperty(PropertyName = "jobTitle")]
        public string JobTitle { get; set; }
    }
}