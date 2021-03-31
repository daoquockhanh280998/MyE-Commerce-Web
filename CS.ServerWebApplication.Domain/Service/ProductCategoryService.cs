using AdminApp.Core.UoW;
using AutoMapper;
using CS.Common.StorageService;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Server.Domain.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        private readonly IStorageService _storageService;
        private const string FOLDER_NAME = "product-content";

        public ProductCategoryService(IUnitOfWork unitOfWork,
            IMapper mapper, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService;
        }
        public ProductCategory Add(ProductCategory entity)
        {
            _unitOfWork.GetRepository<ProductCategory>().Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public async Task<ProductCategory> AddAsync(ProductCategoryRequest request)
        {
            var productCategory = new ProductCategory()
            {
                Id = Guid.NewGuid(),
                ProductCategoryName = request.ProductCategoryName,
                IsShowOnHome = request.IsShowOnHome,
                ProductMainCategoryId = request.ProductMainCategoryId,
                SortOrder=request.SortOrder,
                CreateBy = "Admin",
                Status = request.Status
            };
            _unitOfWork.GetRepository<ProductCategory>().Add(productCategory);

            await _unitOfWork.CommitAsync();

            return productCategory;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductCategory Find(Expression<Func<ProductCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductCategory> FindAll(Expression<Func<ProductCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductCategory>> FindAllAsync(Expression<Func<ProductCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> FindAsync(Expression<Func<ProductCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public ProductCategory Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductCategory> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductCategory Update(ProductCategory updated, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> UpdateAsync(ProductCategory updated, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> AddAsync(ProductCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
