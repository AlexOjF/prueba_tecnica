﻿using System;
using System.Collections.Generic;

namespace prueba_tecnica.Entities;

public partial class Matricula
{
    public int Id { get; set; }

    public string IdAspirante { get; set; } = null!;

    public string IdPrograma { get; set; } = null!;

    public DateTime FechaIncripcin { get; set; }

    public DateTime LimitePago { get; set; }

    public double CostoMatricula { get; set; }

    public bool Pago { get; set; }

    public virtual Aspirante IdAspiranteNavigation { get; set; } = null!;

    public virtual Programa IdProgramaNavigation { get; set; } = null!;
}
