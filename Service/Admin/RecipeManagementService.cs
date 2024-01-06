using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class RecipeManagementService: IRecipeManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public RecipeManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Recipe> GetAllRecipes()
    {
        return _db.Recipes.Include(f=>f.User)
            .ThenInclude(f=>f!.Role)
            .Include(f=>f.CategoryRecipe)
            .OrderByDescending(f=>f.CreatedAt)
            .ToList();
    }

    public Recipe GetRecipe(int id)
    {
        return _db.Recipes.Include(r=>r.Feedbacks)
            .ThenInclude(r=>r.User)
            .Include(f=>f.User).Include(r=>r.User!.Role)
            .Include(f=>f.CategoryRecipe)
            .SingleOrDefault(r => r.RecipeId == id)!;
    }

    public IPagedList<Recipe> PagedList(int page, int pageSize, List<Recipe> recipes)
    {
        return recipes.ToPagedList(page, pageSize);
    }

    public List<Recipe> Sorting(List<Recipe> recipes, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                recipes = recipes.OrderByDescending(r => r.Title).ToList();
                break;
            case "name_asc":
                recipes = recipes.OrderBy(r => r.Title).ToList();
                break;
            case "Date":
                recipes = recipes.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                recipes = recipes.OrderByDescending(r => r.CreatedAt).ToList();
                break;
            case "rating":
                recipes = recipes.OrderBy(r => r.Rating).ToList();
                break;
            case "rating_desc":
                recipes = recipes.OrderByDescending(r => r.Rating).ToList();
                break;
            default:
                recipes = recipes.OrderByDescending(r => r.CreatedAt).ToList();
                break;
        }
        return recipes;
    }

    public List<Recipe> Search(string searchString)
    {
        return _db.Recipes.Where(r => r.Title.Contains(searchString)).ToList();
    }

   public byte[] GeneratedExcel(string fileName, List<Recipe> recipes)
{
    // Tạo một tệp Excel mới
    using (MemoryStream memoryStream = new MemoryStream())
    {
        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
        {
            // Tạo một WorkbookPart để quản lý Workbook
            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            // Tạo một WorksheetPart để quản lý Sheet
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Thêm một Sheet vào Workbook
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
            sheets.Append(sheet);

            // Tạo một danh sách các header
            var headers = new List<string> { "RecipeId", "User", "Role", "Category", "Title", "Ingredients", "Procedure", "Time", "Rating", "Date posted", "Status", "Premium" };

            // Thêm header vào Sheet
            Row headerRow = new Row();
            foreach (var header in headers)
            {
                Cell headerCell = new Cell(new CellValue(header)) { DataType = CellValues.String };
                headerRow.Append(headerCell);
            }
            worksheetPart.Worksheet.Elements<SheetData>().First().Append(headerRow);

            // Lặp qua dữ liệu và thêm nó vào Sheet
            foreach (var recipe in recipes)
            {
                Row dataRow = new Row();
                dataRow.Append(new Cell(new CellValue(recipe.RecipeId.ToString())) { DataType = CellValues.Number });
                dataRow.Append(new Cell(new CellValue(recipe.User!.Username)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.User!.Role!.RoleName)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.CategoryRecipe.CategoryName)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.Title)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.Ingredients)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.Procedure)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.Timeneeds.ToString()!)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.Rating!.ToString()!)) { DataType = CellValues.Number });
                dataRow.Append(new Cell(new CellValue(recipe.CreatedAt.ToString()!)) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.Status.ToString())) { DataType = CellValues.String });
                dataRow.Append(new Cell(new CellValue(recipe.IsMembershipOnly.ToString())) { DataType = CellValues.String });
                worksheetPart.Worksheet.Elements<SheetData>().First().Append(dataRow);
            }

            // Lưu trữ Workbook
            workbookPart.Workbook.Save();
        }

        // Trả về dữ liệu như là một mảng byte
        return memoryStream.ToArray();
    }
}
}