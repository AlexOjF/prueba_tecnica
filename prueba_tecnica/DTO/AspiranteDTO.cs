using prueba_tecnica.Entities;

namespace prueba_tecnica.DTO
{
    public class AspiranteDTO
    {
        public string Id { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Celular { get; set; } = null!;

        public string Correo { get; set; } = null!;
    }
}
