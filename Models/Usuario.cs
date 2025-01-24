using System;
using System.Collections.Generic;

namespace EFAPIProject.Models;

public partial class Usuario
{
    public int Usuarioid { get; set; }

    public string? NomeUsu { get; set; }

    public string? ApelidoUsu { get; set; }

    public string? SenhaUsu { get; set; }

    public string? EmailUsu { get; set; }
}
