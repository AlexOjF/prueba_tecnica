using System;
using System.Collections.Generic;

namespace prueba_tecnica.Entities;

public partial class Programa
{
    public string Id { get; set; } = null!;

    public string Descripcin { get; set; } = null!;

    public string Nivel { get; set; } = null!;

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
