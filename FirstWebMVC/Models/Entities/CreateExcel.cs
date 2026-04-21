using OfficeOpenXml;
using System.IO;

class CreateExcel
{
    static void Main()
    {
        ExcelPackage.License.SetNonCommercialPersonal("Ngoc Duy");

        var file = new FileInfo("students.xlsx");

        using (var package = new ExcelPackage())
        {
            var sheet = package.Workbook.Worksheets.Add("Students");

            // Header
            sheet.Cells[1, 1].Value = "StudentCode";
            sheet.Cells[1, 2].Value = "FullName";
            sheet.Cells[1, 3].Value = "FacultyId";

            // Data mẫu
            sheet.Cells[2, 1].Value = "SV100";
            sheet.Cells[2, 2].Value = "Nguyen Van A";
            sheet.Cells[2, 3].Value = 1;

            sheet.Cells[3, 1].Value = "SV101";
            sheet.Cells[3, 2].Value = "Tran Van B";
            sheet.Cells[3, 3].Value = 2;

            package.SaveAs(file); // ✅ QUAN TRỌNG
        }

        Console.WriteLine("Tạo file Excel thành công!");
    }
}