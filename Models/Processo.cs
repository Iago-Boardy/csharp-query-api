using System;
using System.Collections.Generic;

namespace EFAPIProject.Models;

public partial class Processo
{
    public int Processoid { get; set; }

    public int NroPro { get; set; }

    public int AnoPro { get; set; }

    public int? CodImportador { get; set; }

    public int? CodExportador { get; set; }

    public DateTime? DtAbPro { get; set; }

    public DateTime? DtEncPro { get; set; }

    public DateTime? DtLibPro { get; set; }

    public int? ProcessoUsuario { get; set; }

    public string? IdentCliPro { get; set; }

    public virtual Empresa? CodExportadorNavigation { get; set; }

    public virtual Empresa? CodImportadorNavigation { get; set; }

    public virtual ICollection<Processotracking> Processotrackings { get; set; } = new List<Processotracking>();
}
