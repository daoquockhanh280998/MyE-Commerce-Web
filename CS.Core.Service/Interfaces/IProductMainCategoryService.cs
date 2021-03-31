using AdminApp.Core.UoW;
using CS.EF.Models;
using CS.VM.Request;
using System.Threading.Tasks;

namespace CS.Core.Service.Interfaces
{
    public interface IProductMainCategoryService : IService<ProductMainCategory, IRepository<ProductMainCategory>>
    {
        Task<ProductMainCategory> AddAsync(ProductMainCategoryRequest request);

        //Task<List<Product>> AddListAsync(List<Product> request);

        //PageResult<Product> GetAllPagging(PaggingRequest request);

        //Task<TableResultJsonResponse<ProductViewModel>> GetAllAsync(DataTableParameters parameters);

        //Task<Product> ChangeStatus(Guid id);

        //Task<ICollection<ProductViewModel>> Export();

        //Task<Product> UpdateProductAsync(ProductRequest updated, Guid id);
    }
}