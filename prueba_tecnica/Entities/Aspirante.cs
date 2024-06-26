﻿using System;
using System.Collections.Generic;

namespace prueba_tecnica.Entities;

public partial class Aspirante
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
