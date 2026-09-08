using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamevault_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategoryName",
                table: "SubCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryTitle",
                table: "Categories",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CategoryImagePublicId",
                table: "Categories",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "CategoryImage",
                table: "Categories",
                newName: "ImagePublicId");

            migrationBuilder.AddColumn<int>(
                name: "ActiveProducts",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveProducts",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubCategories",
                newName: "SubCategoryName");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Categories",
                newName: "CategoryTitle");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Categories",
                newName: "CategoryImagePublicId");

            migrationBuilder.RenameColumn(
                name: "ImagePublicId",
                table: "Categories",
                newName: "CategoryImage");
        }
    }
}
