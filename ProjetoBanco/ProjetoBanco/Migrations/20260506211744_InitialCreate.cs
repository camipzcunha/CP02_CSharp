using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBanco.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_AGENCIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AGENCIA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    TipoProduto = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    TaxaJurosAnual = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    PrazoMaximoMeses = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_CLIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    AgenciaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TipoCliente = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Cnpj = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: true),
                    RazaoSocial = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CLIENTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_CLIENTE_TB_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "TB_AGENCIA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_CONTRATACAO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProdutoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CONTRATACAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_CONTRATACAO_TB_CLIENTE_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "TB_CLIENTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_CONTRATACAO_TB_PRODUTO_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "TB_PRODUTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_PRODUTO",
                columns: new[] { "Id", "Descricao", "Nome", "PrazoMaximoMeses", "TaxaJurosAnual", "TipoProduto", "ValorMaximo" },
                values: new object[] { 1, "Empréstimo pessoal com análise de crédito", "Empréstimo Pessoal", 60, 18.5m, "EMPRESTIMO", 50000m });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CLIENTE_AgenciaId",
                table: "TB_CLIENTE",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTRATACAO_ClienteId",
                table: "TB_CONTRATACAO",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTRATACAO_ProdutoId",
                table: "TB_CONTRATACAO",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CONTRATACAO");

            migrationBuilder.DropTable(
                name: "TB_CLIENTE");

            migrationBuilder.DropTable(
                name: "TB_PRODUTO");

            migrationBuilder.DropTable(
                name: "TB_AGENCIA");
        }
    }
}
