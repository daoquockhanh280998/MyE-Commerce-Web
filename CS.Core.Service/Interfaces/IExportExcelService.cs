using CS.VM.ProductViewModel;
using CS.VM.Rerponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Core.Service.Interfaces
{
    public interface IExportExcelService
    {
        ExportResponse ExportProduct(ICollection<ProductViewModel> Products);
    }
}
