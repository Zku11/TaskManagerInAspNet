using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerInAspNet.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                IF NOT EXISTS (SELECT Id FROM AspNetRoles WHERE Id = '97a32ad4-c8d0-4c9e-9729-d2a13c4f90ad')
                BEGIN
	                INSERT AspNetRoles(Id, [Name], [NormalizedName])
	                VALUES ('97a32ad4-c8d0-4c9e-9729-d2a13c4f90ad', 'admin', 'ADMIN')
                END"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = '97a32ad4-c8d0-4c9e-9729-d2a13c4f90ad'");
        }
    }
}
