using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<int>(type: "int", nullable: true),
                    TravelTo_Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelTo_Type = table.Column<int>(type: "int", nullable: false),
                    TravelTo_DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelTo_DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelTo_ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelTo_ArrivalLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelHome_Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelHome_Type = table.Column<int>(type: "int", nullable: false),
                    TravelHome_DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelHome_DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelHome_ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelHome_ArrivalLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPlans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelPlans");
        }
    }
}
