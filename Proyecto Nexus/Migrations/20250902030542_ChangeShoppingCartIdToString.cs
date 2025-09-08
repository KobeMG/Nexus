using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Nexus.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShoppingCartIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar la clave foránea en la tabla 'CartItems' que referencia a 'ShoppingCarts'
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems");

            // Eliminar la clave primaria de la tabla 'ShoppingCarts' para poder modificar la columna 'Id'.
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            // Eliminar la columna 'Id' para poder recrearla como string.
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCarts");

            // Agregar una nueva columna 'Id' de tipo 'string' a la tabla 'ShoppingCarts'.
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // Agregar la clave primaria a la nueva columna 'Id'.
            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            // Modificar la columna 'ShoppingCartId' en la tabla 'CartItems' a tipo 'string'.
            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Volver a crear la clave foránea con los tipos de datos actualizados.
            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar la clave foránea que referencia a la columna de tipo string.
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems");

            // Eliminar la clave primaria de la tabla 'ShoppingCarts' para poder revertir la columna 'Id'.
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            // Eliminar la columna 'Id' de tipo 'string'.
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCarts");

            // Agregar la columna 'Id' original de tipo 'int' con la propiedad IDENTITY.
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Agregar la clave primaria a la columna 'Id' de tipo 'int'.
            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            // Modificar la columna 'ShoppingCartId' en la tabla 'CartItems' de vuelta a tipo 'int'.
            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Volver a crear la clave foránea con los tipos de datos originales.
            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}