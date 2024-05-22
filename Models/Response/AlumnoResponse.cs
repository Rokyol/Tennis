using System.Text.Json.Serialization;

namespace Tennis.API.Models.Response
{
    public class AlumnoResponse
    {
        [JsonPropertyName("name")]
        public string Nombre { get; set; }
        [JsonPropertyName("surname")]
        public string Apellido { get; set; }

    }
}