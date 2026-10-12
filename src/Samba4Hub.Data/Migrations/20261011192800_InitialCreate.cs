using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Samba4Hub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instrumento",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    categoria = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrumento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    senha_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cpf_cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfil_contratante",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    nome_responsavel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    nome_comercial = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bairro = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perfil_contratante", x => x.id);
                    table.ForeignKey(
                        name: "fk_perfil_contratante_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "perfil_musico",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    nome_artistico = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    modalidade = table.Column<int>(type: "integer", nullable: false),
                    cache_minimo = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    cache_grupo_minimo = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    link_instagram = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    link_youtube = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perfil_musico", x => x.id);
                    table.ForeignKey(
                        name: "fk_perfil_musico_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vaga",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contratante_id = table.Column<long>(type: "bigint", nullable: false),
                    titulo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    descricao = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    estilo_predominante = table.Column<int>(type: "integer", nullable: false),
                    formato_solicitado = table.Column<int>(type: "integer", nullable: false),
                    data_evento = table.Column<DateOnly>(type: "date", nullable: false),
                    hora_inicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    hora_fim = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    logradouro = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    bairro = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    cache_oferecido = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    cache_a_combinar = table.Column<bool>(type: "boolean", nullable: false),
                    possui_equipamento_som = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaga", x => x.id);
                    table.ForeignKey(
                        name: "fk_vaga_perfil_contratante",
                        column: x => x.contratante_id,
                        principalTable: "perfil_contratante",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "musico_instrumento",
                columns: table => new
                {
                    perfil_musico_id = table.Column<long>(type: "bigint", nullable: false),
                    instrumento_id = table.Column<long>(type: "bigint", nullable: false),
                    eh_principal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musico_instrumento", x => new { x.perfil_musico_id, x.instrumento_id });
                    table.ForeignKey(
                        name: "fk_musico_instrumento_instrumento",
                        column: x => x.instrumento_id,
                        principalTable: "instrumento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_musico_instrumento_perfil_musico",
                        column: x => x.perfil_musico_id,
                        principalTable: "perfil_musico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidatura",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vaga_id = table.Column<long>(type: "bigint", nullable: false),
                    musico_id = table.Column<long>(type: "bigint", nullable: false),
                    valor_proposta = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    mensagem = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    data_resposta = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidatura", x => x.id);
                    table.ForeignKey(
                        name: "fk_candidatura_perfil_musico",
                        column: x => x.musico_id,
                        principalTable: "perfil_musico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_candidatura_vaga",
                        column: x => x.vaga_id,
                        principalTable: "vaga",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vaga_instrumento",
                columns: table => new
                {
                    vaga_id = table.Column<long>(type: "bigint", nullable: false),
                    instrumento_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaga_instrumento", x => new { x.vaga_id, x.instrumento_id });
                    table.ForeignKey(
                        name: "fk_vaga_instrumento_instrumento",
                        column: x => x.instrumento_id,
                        principalTable: "instrumento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_vaga_instrumento_vaga",
                        column: x => x.vaga_id,
                        principalTable: "vaga",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interacao_contato",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    candidatura_id = table.Column<long>(type: "bigint", nullable: false),
                    iniciado_por = table.Column<int>(type: "integer", nullable: false),
                    data_clique = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interacao_contato", x => x.id);
                    table.ForeignKey(
                        name: "fk_interacao_contato_candidatura",
                        column: x => x.candidatura_id,
                        principalTable: "candidatura",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "instrumento",
                columns: new[] { "id", "categoria", "data_atualizacao", "data_criacao", "nome" },
                values: new object[,]
                {
                    { 1L, 1, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cavaquinho" },
                    { 2L, 1, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Banjo" },
                    { 3L, 1, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Violão 6 Cordas" },
                    { 4L, 1, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Violão 7 Cordas" },
                    { 5L, 1, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Bandolim" },
                    { 6L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pandeiro" },
                    { 7L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Surdo" },
                    { 8L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tantã" },
                    { 9L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Repique de Mão" },
                    { 10L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Rebolo" },
                    { 11L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tamborim" },
                    { 12L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Reco-reco" },
                    { 13L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Ganzá" },
                    { 14L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cuíca" },
                    { 15L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Agogô" },
                    { 16L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Timbal" },
                    { 17L, 2, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Atabaque" },
                    { 18L, 3, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Voz Principal" },
                    { 19L, 3, null, new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Backing Vocal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidatura_musico_id",
                table: "candidatura",
                column: "musico_id");

            migrationBuilder.CreateIndex(
                name: "ux_candidatura_vaga_id_musico_id",
                table: "candidatura",
                columns: new[] { "vaga_id", "musico_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_instrumento_nome",
                table: "instrumento",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_interacao_contato_candidatura_id",
                table: "interacao_contato",
                column: "candidatura_id");

            migrationBuilder.CreateIndex(
                name: "IX_musico_instrumento_instrumento_id",
                table: "musico_instrumento",
                column: "instrumento_id");

            migrationBuilder.CreateIndex(
                name: "ux_musico_instrumento_principal",
                table: "musico_instrumento",
                column: "perfil_musico_id",
                unique: true,
                filter: "eh_principal = true");

            migrationBuilder.CreateIndex(
                name: "ux_perfil_contratante_usuario_id",
                table: "perfil_contratante",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_perfil_musico_usuario_id",
                table: "perfil_musico",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_usuario_cpf_cnpj",
                table: "usuario",
                column: "cpf_cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_usuario_email",
                table: "usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vaga_contratante_id",
                table: "vaga",
                column: "contratante_id");

            migrationBuilder.CreateIndex(
                name: "ix_vaga_status_cidade_data_evento",
                table: "vaga",
                columns: new[] { "status", "cidade", "data_evento" });

            migrationBuilder.CreateIndex(
                name: "IX_vaga_instrumento_instrumento_id",
                table: "vaga_instrumento",
                column: "instrumento_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interacao_contato");

            migrationBuilder.DropTable(
                name: "musico_instrumento");

            migrationBuilder.DropTable(
                name: "vaga_instrumento");

            migrationBuilder.DropTable(
                name: "candidatura");

            migrationBuilder.DropTable(
                name: "instrumento");

            migrationBuilder.DropTable(
                name: "perfil_musico");

            migrationBuilder.DropTable(
                name: "vaga");

            migrationBuilder.DropTable(
                name: "perfil_contratante");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
