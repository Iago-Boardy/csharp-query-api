using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace EFAPIProject.Models;

public partial class DesafioidatadbContext : DbContext
{
    public DesafioidatadbContext()
    {
    }

    public DesafioidatadbContext(DbContextOptions<DesafioidatadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Processo> Processos { get; set; }

    public virtual DbSet<Processotracking> Processotrackings { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.CodEmp).HasName("PRIMARY");

            entity
                .ToTable("empresa")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.HasIndex(e => e.FantasiaEmp, "AK_empresa_fantasia_emp");

            entity.HasIndex(e => e.RazaosocialEmp, "AK_empresa_razaosocial_emp");

            entity.HasIndex(e => e.TipopessoaEmp, "AK_empresa_tipopessoa_emp");

            entity.Property(e => e.CodEmp).HasColumnName("cod_emp");
            entity.Property(e => e.FantasiaEmp)
                .HasMaxLength(50)
                .HasColumnName("fantasia_emp");
            entity.Property(e => e.RazaosocialEmp)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("razaosocial_emp");
            entity.Property(e => e.TelefoneEmp)
                .HasMaxLength(100)
                .HasColumnName("telefone_emp");
            entity.Property(e => e.TipopessoaEmp)
                .HasMaxLength(30)
                .HasColumnName("tipopessoa_emp");
        });

        modelBuilder.Entity<Processo>(entity =>
        {
            entity.HasKey(e => e.Processoid).HasName("PRIMARY");

            entity
                .ToTable("processo")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.HasIndex(e => e.CodExportador, "AK_processo_cod_exportador");

            entity.HasIndex(e => e.CodImportador, "AK_processo_cod_importador");

            entity.HasIndex(e => e.DtAbPro, "AK_processo_dt_ab_pro");

            entity.HasIndex(e => e.DtEncPro, "AK_processo_dt_enc_pro");

            entity.HasIndex(e => e.DtLibPro, "AK_processo_dt_lib_pro");

            entity.HasIndex(e => e.IdentCliPro, "AK_processo_ident_cli_pro");

            entity.HasIndex(e => new { e.NroPro, e.AnoPro }, "AK_processo_nro_pro_ano_pro").IsUnique();

            entity.Property(e => e.Processoid)
                .ValueGeneratedNever()
                .HasColumnName("processoid");
            entity.Property(e => e.AnoPro).HasColumnName("ano_pro");
            entity.Property(e => e.CodExportador).HasColumnName("cod_exportador");
            entity.Property(e => e.CodImportador).HasColumnName("cod_importador");
            entity.Property(e => e.DtAbPro)
                .HasColumnType("datetime")
                .HasColumnName("dt_ab_pro");
            entity.Property(e => e.DtEncPro)
                .HasColumnType("datetime")
                .HasColumnName("dt_enc_pro");
            entity.Property(e => e.DtLibPro)
                .HasColumnType("datetime")
                .HasColumnName("dt_lib_pro");
            entity.Property(e => e.IdentCliPro).HasColumnName("ident_cli_pro");
            entity.Property(e => e.NroPro).HasColumnName("nro_pro");
            entity.Property(e => e.ProcessoUsuario).HasColumnName("processo_usuario");

            entity.HasOne(d => d.CodExportadorNavigation).WithMany(p => p.ProcessoCodExportadorNavigations)
                .HasForeignKey(d => d.CodExportador)
                .HasConstraintName("FK_processo_empresaexportador");

            entity.HasOne(d => d.CodImportadorNavigation).WithMany(p => p.ProcessoCodImportadorNavigations)
                .HasForeignKey(d => d.CodImportador)
                .HasConstraintName("FK_processo_empresaimportador");
        });

        modelBuilder.Entity<Processotracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("processotracking")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.HasIndex(e => new { e.NroPro, e.AnoPro, e.Ordem }, "AK_processotracking_nro_pro_ano_pro_ordem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoPro).HasColumnName("ano_pro");
            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.Confirmacaodata)
                .HasColumnType("datetime")
                .HasColumnName("confirmacaodata");
            entity.Property(e => e.Liberacaodata).HasColumnName("liberacaodata");
            entity.Property(e => e.Navioid).HasColumnName("navioid");
            entity.Property(e => e.Naviovoo)
                .HasMaxLength(50)
                .HasColumnName("naviovoo");
            entity.Property(e => e.NroPro).HasColumnName("nro_pro");
            entity.Property(e => e.Ordem).HasColumnName("ordem");
            entity.Property(e => e.Portoaeroportoid).HasColumnName("portoaeroportoid");
            entity.Property(e => e.Previsaodata)
                .HasColumnType("datetime")
                .HasColumnName("previsaodata");
            entity.Property(e => e.Tag).HasDefaultValueSql("'0'");
            entity.Property(e => e.UltimaAlteracao)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Processo).WithMany(p => p.Processotrackings)
                .HasPrincipalKey(p => new { p.NroPro, p.AnoPro })
                .HasForeignKey(d => new { d.NroPro, d.AnoPro })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_processotracking_processo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuarioid).HasName("PRIMARY");

            entity
                .ToTable("usuario")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.HasIndex(e => e.ApelidoUsu, "AK_usuario_apelido_usu").IsUnique();

            entity.HasIndex(e => e.NomeUsu, "AK_usuario_nome_usu");

            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");
            entity.Property(e => e.ApelidoUsu)
                .HasMaxLength(50)
                .HasColumnName("apelido_usu");
            entity.Property(e => e.EmailUsu)
                .HasMaxLength(50)
                .HasColumnName("email_usu");
            entity.Property(e => e.NomeUsu)
                .HasMaxLength(50)
                .HasColumnName("nome_usu");
            entity.Property(e => e.SenhaUsu)
                .HasMaxLength(8)
                .HasColumnName("senha_usu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
