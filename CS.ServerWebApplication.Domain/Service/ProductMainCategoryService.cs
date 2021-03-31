using AdminApp.Core.UoW;
using AutoMapper;
using CS.Common.StorageService;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CS.Server.Domain.Service
{
    public class ProductMainCategoryService : IProductMainCategoryService
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

        public ProductMainCategoryService(IUnitOfWork unitOfWork,
            IMapper mapper, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService;
        }
        public ProductMainCategory Add(ProductMainCategory entity)
        {
            _unitOfWork.GetRepository<ProductMainCategory>().Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public async Task<ProductMainCategory> AddAsync(ProductMainCategoryRequest request)
        {
            var productMainCategory = new ProductMainCategory()
            {
                Id = Guid.NewGuid(),
                ProductMainCategoryName = request.ProductMainCategoryName,
                SortOrder = request.SortOrder,
                CreateBy = "Admin",
                Status = request.Status
            };
            _unitOfWork.GetRepository<ProductMainCategory>().Add(productMainCategory);

            await _unitOfWork.CommitAsync();

            return productMainCategory;
        }

        public Task<ProductMainCategory> AddAsync(ProductMainCategory entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductMainCategory entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(ProductMainCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductMainCategory Find(Expression<Func<ProductMainCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductMainCategory> FindAll(Expression<Func<ProductMainCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductMainCategory>> FindAllAsync(Expression<Func<ProductMainCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ProductMainCategory> FindAsync(Expression<Func<ProductMainCategory, bool>> match)
        {
            throw new NotImplementedException();
        }

        public ProductMainCategory Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductMainCategory> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductMainCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductMainCategory> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductMainCategory Update(ProductMainCategory updated, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductMainCategory> UpdateAsync(ProductMainCategory updated, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
