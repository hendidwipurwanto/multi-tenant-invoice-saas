using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantInvoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Auditlog_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "AuditLogs");
        }
    }
}
