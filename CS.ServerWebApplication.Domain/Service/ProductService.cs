using AdminApp.Core.UoW;
using AutoMapper;
using CS.Base;
using CS.Common.StorageService;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.ProductViewModel;
using CS.VM.Request;
using CS.VM.Rerponse;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CS.Server.Domain.Service
{
    public class ProductService : IProductService
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

        public ProductService(IUnitOfWork unitOfWork,
            IMapper mapper, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService;
        }

        public Product Add(Product entity)
        {
            _unitOfWork.GetRepository<Product>().Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public async Task<Product> AddAsync(ProductRequest request)
        {
            var product = new Product()
            {
                ProductID = Guid.NewGuid(),
                ProductName = request.ProductName,
                Price = request.Price,
                OldPrice = request.OldPrice,
                CreateBy = request.CreateBy,
                DateCreated = DateTime.Now,
                Status = true
            };
            _unitOfWork.GetRepository<Product>().Add(product);

            if (request.ThumbnailImage != null)
            {
                var a = await AddProductImage(request, product);
                await _unitOfWork.CommitAsync();
                product.ImagePath = a.ImagePath;
                _unitOfWork.GetRepository<Product>().Update(product);
            }

            await _unitOfWork.CommitAsync();

            return product;
        }

        private async Task<ProductImage> AddProductImage(ProductRequest request, Product product)
        {
            var productImage = new ProductImage()
            {
                Id = Guid.NewGuid(),
                ProductId = product.ProductID,
                Caption = "Thumbnail image",
                DateCreated = DateTime.Now,
                FileSize = request.ThumbnailImage.Length,
                ImagePath = await this.SaveFile(request.ThumbnailImage),
                IsDefault = true,
                SortOrder = 1
            };
            _unitOfWork.GetRepository<ProductImage>().Add(productImage);

            return productImage;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName, FOLDER_NAME);
            return "/" + FOLDER_NAME + "/" + fileName;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            _unitOfWork.GetRepository<Product>().Add(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public PageResult<Product> GetAllPagging(PaggingRequest request)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _unitOfWork.GetRepository<Product>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _unitOfWork.GetRepository<Product>().CountAsync();
        }

        public void Delete(Product entity)
        {
            _unitOfWork.GetRepository<Product>().Delete(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.GetRepository<Product>().Delete(id);
            _unitOfWork.Commit();
        }

        public async Task<int> DeleteAsync(Product entity)
        {
            _unitOfWork.GetRepository<Product>().Delete(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            _unitOfWork.GetRepository<Product>().Delete(id);
            return await _unitOfWork.CommitAsync();
        }

        public Product Find(Expression<Func<Product, bool>> match)
        {
            return _unitOfWork.GetRepository<Product>().Find(match);
        }

        public ICollection<Product> FindAll(Expression<Func<Product, bool>> match)
        {
            return _unitOfWork.GetRepository<Product>().GetAll().Where(match).ToList();
        }

        public async Task<ICollection<Product>> FindAllAsync(Expression<Func<Product, bool>> match)
        {
            return await _unitOfWork.GetRepository<Product>().GetAll().Where(match).ToListAsync();
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> match)
        {
            return await _unitOfWork.GetRepository<Product>().FindAsync(match);
        }

        public Product Get(Guid id)
        {
            return _unitOfWork.GetRepository<Product>().GetById(id);
        }

        public ICollection<Product> GetAll()
        {
            return _unitOfWork.GetRepository<Product>().GetAll().ToList();
        }

        public async Task<TableResultJsonResponse<ProductViewModel>> GetAllAsync(DataTableParameters parameters)
        {
            var data = new ConcurrentBag<ProductViewModel>();
            var result = new TableResultJsonResponse<ProductViewModel>();
            var products = _unitOfWork.GetRepository<Product>().GetAll();
            if (!string.IsNullOrEmpty(parameters.Search.Value) || parameters.Search.Value == "string")
            {
                products = _unitOfWork.GetRepository<Product>().GetAll().Where(x => x.ProductName == parameters.Search.Value );
            }
            else
            {
                products = _unitOfWork.GetRepository<Product>().GetAll();
            }
            var totalRecord = products.Count();
            var filteredProducts = await products.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            // var productsImgage = _unitOfWork.GetRepository<ProductImage>().GetAll();
            foreach (var product in filteredProducts)
            {
                var productInfo = _mapper.Map<ProductViewModel>(product);
                //var productInfo = new ProductViewModel()
                //{
                //    ProductID = product.ProductID,
                //    ProductName = product.ProductName,
                //    //ImagePath = productsImgage.
                //};
                data.Add(productInfo);
            }
            result.Draw = parameters.Draw;
            result.RecordsTotal = totalRecord;
            result.RecordsFiltered = totalRecord;
            result.Data = data.ToList();

            return result;
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Product>().GetAsyncById(id);
        }

        public Product Update(Product updated, Guid id)
        {
            if (updated == null)
                return null;

            Product existing = _unitOfWork.GetRepository<Product>().GetById(id);
            if (existing != null)
            {
                _unitOfWork.GetRepository<Product>().Update(updated);
                _unitOfWork.Commit();
            }

            return existing;
        }

        public async Task<Product> UpdateProductAsync(ProductRequest updated, Guid id)
        {
            if (updated == null)
                return null;

            Product existing = await _unitOfWork.GetRepository<Product>().GetAsyncById(id);
            if (existing != null)
            {
                existing.ProductName = updated.ProductName;
                existing.Price = updated.Price;
                existing.OldPrice = updated.OldPrice;
                if (updated.ThumbnailImage != null)
                {
                    existing.ImagePath = await this.SaveFile(updated.ThumbnailImage);
                }
                existing.UpdateDate = DateTime.Now;
                existing.UpdateBy = "Admin1";
                _unitOfWork.GetRepository<Product>().Update(existing);
                await _unitOfWork.CommitAsync();
            }
            return existing;
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<Product>().GetAll().Where(x => x.Status == true).ToListAsync();
        }

        public async Task<Product> ChangeStatus(Guid id)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetAsyncById(id);
            if (product != null)
            {
                product.Status = !product.Status;
                _unitOfWork.GetRepository<Product>().Update(product);
                await _unitOfWork.CommitAsync();
            }

            return product;
        }

        public async Task<ICollection<ProductViewModel>> Export()
        {
            var productViewModels = new List<ProductViewModel>();
            var products = await _unitOfWork.GetRepository<Product>().GetAll().ToListAsync();

            foreach (var product in products)
            {
                var productInfo = _mapper.Map<ProductViewModel>(product);
                productViewModels.Add(productInfo);
            }

            return productViewModels;
        }

        public async Task<List<Product>> AddListAsync(List<Product> request)
        {
            var products = new List<Product>();
            foreach (var item in request)
            {
                var productInfo = _mapper.Map<Product>(item);
                products.Add(productInfo);
                _unitOfWork.GetRepository<Product>().Add(productInfo);
            }
            await _unitOfWork.CommitAsync();

            return products;
        }

        public Task<Product> UpdateAsync(Product updated, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}