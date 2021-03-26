using AdminApp.Core.UoW;
using CS.Base;
using CS.EF.Models;
using CS.VM.ProductViewModel;
using CS.VM.Request;
using CS.VM.Rerponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CS.Core.Service.Interfaces
{
    public interface IProductService : IService<Product, IRepository<Product>>
    {
        Task<Product> AddAsync(ProductRequest request);

        Task<List<Product>> AddListAsync(List<Product> request);

        PageResult<Product> GetAllPagging(PaggingRequest request);

        Task<TableResultJsonResponse<ProductViewModel>> GetAllAsync(DataTableParameters parameters);

        Task<Product> ChangeStatus(Guid id);

        Task<ICollection<ProductViewModel>> Export();

        Task<Product> UpdateProductAsync(ProductRequest updated, Guid id);
    }
}