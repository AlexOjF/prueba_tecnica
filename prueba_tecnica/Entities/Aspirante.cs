using System;
using System.Collections.Generic;

namespace prueba_tecnica.Entities;

public partial class Aspirante
{
    public string IdAspirante { get; set; } = null!;

    public string Nombre1 { get; set; } = null!;

    public string? Nombre2 { get; set; }

    public string Apellido1 { get; set; } = null!;

    public string? Apellido2 { get; set; }

    public string Celular { get; set; } = null!;

    public string Correo { get; set; } = null!;
}
