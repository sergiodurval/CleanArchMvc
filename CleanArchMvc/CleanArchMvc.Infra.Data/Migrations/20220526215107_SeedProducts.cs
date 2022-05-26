using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) values('Caderno espiral','Caderno espiral 100 folhas','7.45',50,'caderno1.jpg',1)");
            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) values('Estojo escolar','Estojo escolar cinza','5.60',70,'estojo1.jpg',1)");
            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) values('Borracha escolar','Borracha branca pequena','3.25',80,'borracha01.jpg',1)");
            migrationBuilder.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) values('Calculadora escolar','Calculadora simples','15.39',20,'calculadora.jpg',2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
