using EFAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly DesafioidatadbContext _context;

        public QueryController(DesafioidatadbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessos()
        {
            try
            {
                var result = await (from p in _context.Processos
                                    join e1 in _context.Empresas on p.CodImportador equals e1.CodEmp
                                    join e2 in _context.Empresas on p.CodExportador equals e2.CodEmp
                                    join u in _context.Usuarios on p.ProcessoUsuario equals u.Usuarioid
                                    join pk1 in _context.Processotrackings
                                        on new { p.NroPro, p.AnoPro, Ordem = 1 }
                                        equals new { pk1.NroPro, pk1.AnoPro, pk1.Ordem }
                                    join pk2 in _context.Processotrackings
                                        on new { p.NroPro, p.AnoPro }
                                        equals new { pk2.NroPro, pk2.AnoPro }
                                    where pk2.Ordem == _context.Processotrackings
                                        .Where(x => x.NroPro == p.NroPro && x.AnoPro == p.AnoPro)
                                        .Max(x => x.Ordem)
                                    select new
                                    {
                                        p.Processoid,
                                        p.NroPro,
                                        p.AnoPro,
                                        ConfirmacaoEmbarque = pk1.Confirmacaodata,
                                        ConfirmacaoChegada = pk2.Confirmacaodata,
                                        p.CodImportador,
                                        NomeEmpresaImport = e1.RazaosocialEmp,
                                        p.CodExportador,
                                        NomeEmpresaExport = e2.RazaosocialEmp,
                                        p.DtAbPro,
                                        p.DtEncPro,
                                        p.DtLibPro,
                                        p.ProcessoUsuario,
                                        NomeUsuario = u.NomeUsu,
                                        p.IdentCliPro
                                    }).ToListAsync();    

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
