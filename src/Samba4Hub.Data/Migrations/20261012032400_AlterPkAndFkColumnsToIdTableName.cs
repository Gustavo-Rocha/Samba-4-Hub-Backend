using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samba4Hub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterPkAndFkColumnsToIdTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "instrumento_id",
                table: "vaga_instrumento",
                newName: "id_instrumento");

            migrationBuilder.RenameColumn(
                name: "vaga_id",
                table: "vaga_instrumento",
                newName: "id_vaga");

            migrationBuilder.RenameIndex(
                name: "IX_vaga_instrumento_instrumento_id",
                table: "vaga_instrumento",
                newName: "IX_vaga_instrumento_id_instrumento");

            migrationBuilder.RenameColumn(
                name: "contratante_id",
                table: "vaga",
                newName: "id_perfil_contratante");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "vaga",
                newName: "id_vaga");

            migrationBuilder.RenameIndex(
                name: "IX_vaga_contratante_id",
                table: "vaga",
                newName: "IX_vaga_id_perfil_contratante");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "usuario",
                newName: "id_usuario");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "perfil_musico",
                newName: "id_usuario");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "perfil_musico",
                newName: "id_perfil_musico");

            migrationBuilder.RenameIndex(
                name: "ux_perfil_musico_usuario_id",
                table: "perfil_musico",
                newName: "ux_perfil_musico_id_usuario");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "perfil_contratante",
                newName: "id_usuario");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "perfil_contratante",
                newName: "id_perfil_contratante");

            migrationBuilder.RenameIndex(
                name: "ux_perfil_contratante_usuario_id",
                table: "perfil_contratante",
                newName: "ux_perfil_contratante_id_usuario");

            migrationBuilder.RenameColumn(
                name: "instrumento_id",
                table: "musico_instrumento",
                newName: "id_instrumento");

            migrationBuilder.RenameColumn(
                name: "perfil_musico_id",
                table: "musico_instrumento",
                newName: "id_perfil_musico");

            migrationBuilder.RenameIndex(
                name: "IX_musico_instrumento_instrumento_id",
                table: "musico_instrumento",
                newName: "IX_musico_instrumento_id_instrumento");

            migrationBuilder.RenameColumn(
                name: "candidatura_id",
                table: "interacao_contato",
                newName: "id_candidatura");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "interacao_contato",
                newName: "id_interacao_contato");

            migrationBuilder.RenameIndex(
                name: "ix_interacao_contato_candidatura_id",
                table: "interacao_contato",
                newName: "ix_interacao_contato_id_candidatura");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "instrumento",
                newName: "id_instrumento");

            migrationBuilder.RenameColumn(
                name: "vaga_id",
                table: "candidatura",
                newName: "id_vaga");

            migrationBuilder.RenameColumn(
                name: "musico_id",
                table: "candidatura",
                newName: "id_perfil_musico");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "candidatura",
                newName: "id_candidatura");

            migrationBuilder.RenameIndex(
                name: "ux_candidatura_vaga_id_musico_id",
                table: "candidatura",
                newName: "ux_candidatura_id_vaga_id_perfil_musico");

            migrationBuilder.RenameIndex(
                name: "IX_candidatura_musico_id",
                table: "candidatura",
                newName: "IX_candidatura_id_perfil_musico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_instrumento",
                table: "vaga_instrumento",
                newName: "instrumento_id");

            migrationBuilder.RenameColumn(
                name: "id_vaga",
                table: "vaga_instrumento",
                newName: "vaga_id");

            migrationBuilder.RenameIndex(
                name: "IX_vaga_instrumento_id_instrumento",
                table: "vaga_instrumento",
                newName: "IX_vaga_instrumento_instrumento_id");

            migrationBuilder.RenameColumn(
                name: "id_perfil_contratante",
                table: "vaga",
                newName: "contratante_id");

            migrationBuilder.RenameColumn(
                name: "id_vaga",
                table: "vaga",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_vaga_id_perfil_contratante",
                table: "vaga",
                newName: "IX_vaga_contratante_id");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "usuario",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "perfil_musico",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "id_perfil_musico",
                table: "perfil_musico",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ux_perfil_musico_id_usuario",
                table: "perfil_musico",
                newName: "ux_perfil_musico_usuario_id");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "perfil_contratante",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "id_perfil_contratante",
                table: "perfil_contratante",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ux_perfil_contratante_id_usuario",
                table: "perfil_contratante",
                newName: "ux_perfil_contratante_usuario_id");

            migrationBuilder.RenameColumn(
                name: "id_instrumento",
                table: "musico_instrumento",
                newName: "instrumento_id");

            migrationBuilder.RenameColumn(
                name: "id_perfil_musico",
                table: "musico_instrumento",
                newName: "perfil_musico_id");

            migrationBuilder.RenameIndex(
                name: "IX_musico_instrumento_id_instrumento",
                table: "musico_instrumento",
                newName: "IX_musico_instrumento_instrumento_id");

            migrationBuilder.RenameColumn(
                name: "id_candidatura",
                table: "interacao_contato",
                newName: "candidatura_id");

            migrationBuilder.RenameColumn(
                name: "id_interacao_contato",
                table: "interacao_contato",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ix_interacao_contato_id_candidatura",
                table: "interacao_contato",
                newName: "ix_interacao_contato_candidatura_id");

            migrationBuilder.RenameColumn(
                name: "id_instrumento",
                table: "instrumento",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "id_vaga",
                table: "candidatura",
                newName: "vaga_id");

            migrationBuilder.RenameColumn(
                name: "id_perfil_musico",
                table: "candidatura",
                newName: "musico_id");

            migrationBuilder.RenameColumn(
                name: "id_candidatura",
                table: "candidatura",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ux_candidatura_id_vaga_id_perfil_musico",
                table: "candidatura",
                newName: "ux_candidatura_vaga_id_musico_id");

            migrationBuilder.RenameIndex(
                name: "IX_candidatura_id_perfil_musico",
                table: "candidatura",
                newName: "IX_candidatura_musico_id");
        }
    }
}
