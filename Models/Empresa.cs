using System;
using System.Collections.Generic;

namespace EFAPIProject.Models;

public partial class Empresa
{
    public int CodEmp { get; set; }

    public string? TipopessoaEmp { get; set; }

    public string RazaosocialEmp { get; set; } = null!;

    public string? FantasiaEmp { get; set; }

    public string? TelefoneEmp { get; set; }

    public virtual ICollection<Processo> ProcessoCodExportadorNavigations { get; set; } = new List<Processo>();

    public virtual ICollection<Processo> ProcessoCodImportadorNavigations { get; set; } = new List<Processo>();
}
