using Tennis.API.Models.Response.DTO;

namespace Tennis.API.Models.Response
{
    public class AlumnoWithMateriasResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public List<MateriaDTO> Materias { get; set; }
    }
}
