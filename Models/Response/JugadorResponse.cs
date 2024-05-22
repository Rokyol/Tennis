namespace Tennis.Models.Response
{
    public class JugadorResponse
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Genero { get; set; }
        public int Habilidad { get; set; }
        public int Suerte { get; set; }
        public int Fuerza { get; set; }
        public int Velocidad { get; set; }
        public int Reaccion { get; set; }
        public bool Activo { get; set; }
    }
}
