using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.ProductViewModel;
using CS.VM.Rerponse;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Server.Domain.Service
{
    public class ExportExcelService : IExportExcelService
    {
        private readonly int NUMBER_IGNORE_ROW = 16;
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportExcelService"/> class.
        /// </summary>
        public ExportExcelService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ExportResponse ExportProduct(ICollection<ProductViewModel> Products)
        {
            var webRoot = "wwwroot/";
            var fileName = "Tekmedi.DanhSachBenhNhan.xlsx";
            var tempPath = @"templates/" + fileName;
            FileInfo tempFile = new FileInfo(Path.Combine(webRoot, tempPath));
            if (!tempFile.Exists) throw new Exception("Không tìm thấy file template");

            var exportFolder = Path.Combine(webRoot, "export");
            if (!Directory.Exists(exportFolder))
            {
                Directory.CreateDirectory(exportFolder);
            }

            var filePath = @"export/" + fileName;
            var fullPath = Path.Combine(webRoot, filePath);

            FileInfo file = new FileInfo(fullPath);

            if (file.Exists)
            {
                file.Delete();
                tempFile.CopyTo(fullPath);
            }
            else
            {
                tempFile.CopyTo(fullPath);
            }

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var package = new ExcelPackage(file))
            {
                ExcelWorksheet detailSheet = package.Workbook.Worksheets["Chi Tiết"];

                if (Products.Count() != 0)
                {
                    var row = NUMBER_IGNORE_ROW;

                    detailSheet.InsertRow(row + 2, Products.Count() - 2, row + 1); // Have two temp rows

                    foreach (var i in Products)
                    {
                        //NUMBER_IGNORE_ROW bat dau tu dong
                        detailSheet.Cells["B" + row].Value = row - NUMBER_IGNORE_ROW + 1;
                        detailSheet.Cells["C" + row].Value = i.ProductID;
                        detailSheet.Cells["D" + row].Value = i.ProductName;
                        detailSheet.Cells["E" + row].Value = i.Price;
                        detailSheet.Cells["F" + row].Value = i.OldPrice;
                        detailSheet.Cells["G" + row].Value = i.DateCreated.HasValue ? i.DateCreated.Value.ToString("dd/MM/yyyy") : string.Empty;
                        detailSheet.Cells["H" + row].Value = i.CreateBy;
                        detailSheet.Cells["I" + row].Value = i.Status == true ? "Còn Hoạt Động" : "Không Còn Hoạt Động";
                        row++;
                    }

                    detailSheet.Cells[detailSheet.Dimension.Address].AutoFitColumns();

                    var indexAddress = row + 1; // Next one rows
                    detailSheet.Cells["T" + indexAddress].Value = "Product Manager , Ngày " + DateTime.Now.ToString("dd") + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

                    var indexSign = row + 6; // Next six rows
                    //detailSheet.Cells["T" + indexSign].Value = exportRequest.EmployeeName;
                }

                package.Save();
            }

            return new ExportResponse
            {
                FilePath = filePath,
                FileName = fileName
            };
        }

        public async Task<List<Product>> ImportProduct(IFormFile file)
        {
            var listProduct = new List<Product>();
            using (var steam = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                await file.CopyToAsync(steam);
                using (var package = new ExcelPackage(steam))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        listProduct.Add(new Product
                        {
                            ProductID = Guid.NewGuid(),
                            ProductName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Price = Convert.ToDecimal(worksheet.Cells[row, 2].Value),
                            OldPrice = Convert.ToDecimal(worksheet.Cells[row, 3].Value),
                            DateCreated = DateTime.Now,
                            CreateBy = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            Status = true
                        });
                    }
                };
            };

            return listProduct;
        }
    }
}
