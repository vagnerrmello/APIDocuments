using Microsoft.EntityFrameworkCore.Migrations;

namespace APIDocuments.Migrations
{
    public partial class Populardb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into usuarios(Nome, email, password, ativo) " +
                "Values('vagnerrmello', 'vagnerrmello@gmail.com', '12345678', true)");

            migrationBuilder.Sql("Insert Into documentos(Tipo, Vencimento, Pagamento, Path, Status, UsuarioId) " +
                "Values(1, now(), now(), 'C:\\temp', 1, (Select UsuarioId From usuarios Where Nome = 'vagnerrmello'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
