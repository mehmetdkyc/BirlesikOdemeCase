using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class paymentLogstableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentLogs",
                columns: table => new
                {
                    orderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    rnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hostReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    responseHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    responseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    responseMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    extraData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    installmentCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saleDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vPosName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentSystem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLogs", x => x.orderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentLogs");
        }
    }
}
