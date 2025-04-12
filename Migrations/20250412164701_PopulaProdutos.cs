using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Coca-Cola Diet', 'Refrigerante 300ml', 5.45, 'cocacola.jpg', 50, now(), 1)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Pastel', 'Pastel da feira', 2.45, 'pastel.jpg', 10, now(), 2)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Bolo', 'Bolo de Chocolate', 1.45, 'bolo.jpg', 5, now(), 3)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

            mb.Sql("Delete from Produtos");

        }
    }
}
