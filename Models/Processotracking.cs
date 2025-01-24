using System;
using System.Collections.Generic;

namespace EFAPIProject.Models;

public partial class Processotracking
{
    public int Id { get; set; }

    public int NroPro { get; set; }

    public int AnoPro { get; set; }

    public int Portoaeroportoid { get; set; }

    public DateTime? Previsaodata { get; set; }

    public DateTime? Confirmacaodata { get; set; }

    public int? Navioid { get; set; }

    public string? Naviovoo { get; set; }

    public DateOnly? Liberacaodata { get; set; }

    public int Ordem { get; set; }

    public string? Code { get; set; }

    public bool? Tag { get; set; }

    public DateTime? UltimaAlteracao { get; set; }

    public virtual Processo Processo { get; set; } = null!;
}
