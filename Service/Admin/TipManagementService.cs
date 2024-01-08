using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class TipManagementService : ITipManagementRepository
{
    private readonly JamesrecipesContext _db;

    public TipManagementService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Tip> GetAllTip()
    {
        return _db.Tips.Include(f => f.User)
            .ThenInclude(f => f!.Role)
            .Include(f => f.CategoryTip)
            .OrderByDescending(f => f.CreatedAt)
            .ToList();
    }

    public Tip GetTip(int id)
    {
        return _db.Tips.Include(r => r.Feedbacks)
            .ThenInclude(r => r.User)
            .Include(f => f.User).Include(r => r.User!.Role)
            .Include(f => f.CategoryTip)
            .SingleOrDefault(r => r.TipId == id)!;
    }

    public IPagedList<Tip> PagedList(int page, int pageSize, List<Tip> tips)
    {
        return tips.ToPagedList(page, pageSize);

    }


    public List<Tip> Sorting(List<Tip> tips, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                tips = tips.OrderByDescending(r => r.Title).ToList();
                break;
            case "name_asc":
                tips = tips.OrderBy(r => r.Title).ToList();
                break;
            case "Date":
                tips = tips.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                tips = tips.OrderByDescending(r => r.CreatedAt).ToList();
                break;
            case "rating":
                tips = tips.OrderBy(r => r.Rating).ToList();
                break;
            case "rating_desc":
                tips = tips.OrderByDescending(r => r.Rating).ToList();
                break;
            default:
                tips = tips.OrderByDescending(r => r.CreatedAt).ToList();
                break;
        }

        return tips;
    }

    public List<Tip> Search(string searchString)
    {
        return _db.Tips.Where(r => r.Title.Contains(searchString)).ToList();

    }

    public byte[] GeneratedExcel(string filename, List<Tip> tips)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (SpreadsheetDocument spreadsheetDocument =
                   SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                    { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                sheets.Append(sheet);

                var headers = new List<string>
                {
                    "TipId", "User", "Role", "Category", "Title", "Contents", "Rating", "Date posted", "Status",
                    "Premium"
                };

                Row headerRow = new Row();
                foreach (var header in headers)
                {
                    Cell headerCell = new Cell(new CellValue(header)) { DataType = CellValues.String };
                    headerRow.Append(headerCell);
                }

                worksheetPart.Worksheet.Elements<SheetData>().First().Append(headerRow);

                foreach (var tip in tips)
                {
                    Row dataRow = new Row();
                    dataRow.Append(new Cell(new CellValue(tip.TipId.ToString())) { DataType = CellValues.Number });
                    dataRow.Append(new Cell(new CellValue(tip.User!.Username)) { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.User!.Role!.RoleName)) { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.CategoryTip.CategoryName))
                        { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.Title)) { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.Content)) { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.Rating!.ToString()!)) { DataType = CellValues.Number });
                    dataRow.Append(new Cell(new CellValue(tip.CreatedAt.ToString()!)) { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.Status.ToString())) { DataType = CellValues.String });
                    dataRow.Append(new Cell(new CellValue(tip.IsMembershipOnly.ToString()))
                        { DataType = CellValues.String });
                    worksheetPart.Worksheet.Elements<SheetData>().First().Append(dataRow);
                }

                workbookPart.Workbook.Save();
            }

            return memoryStream.ToArray();
        }
    }
}