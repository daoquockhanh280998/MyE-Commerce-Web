using AdminApp.Core.UoW;
using AutoMapper;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ProductService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Product Add(Product entity)
        {
            _unitOfWork.GetRepository<Product>().Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            _unitOfWork.GetRepository<Product>().Add(entity);
            await _unitOfWork.CommitAsync();
            return entity;
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

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<Product>().GetAll().ToListAsync();
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

        public async Task<Product> UpdateAsync(Product updated, Guid id)
        {
            if (updated == null)
                return null;

            Product existing = await _unitOfWork.GetRepository<Product>().GetAsyncById(id);
            if (existing != null)
            {
                existing.ProductName = updated.ProductName;
                existing.Price = updated.Price;
                existing.OldPrice = updated.OldPrice;
                _unitOfWork.GetRepository<Product>().Update(existing);
                await _unitOfWork.CommitAsync();
            }
            return existing;
        }
    }
}