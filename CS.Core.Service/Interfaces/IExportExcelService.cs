using CS.EF.Models;
using CS.VM.ProductViewModel;
using CS.VM.Rerponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CS.Core.Service.Interfaces
{
    public interface IExportExcelService
    {
        ExportResponse ExportProduct(ICollection<ProductViewModel> Products);
        Task<List<Product>> ImportProduct(IFormFile file);
    }
}
