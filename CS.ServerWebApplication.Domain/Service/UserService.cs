using AdminApp.Core.UoW;
using AutoMapper;
using CS.Common.StorageService;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using CS.VM.Rerponse;
using CS.VM.UserViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CS.Server.Domain.Service
{
    public class UserService : IUserService
    {

        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConfiguration _config;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper, IConfiguration config, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _storageService = storageService;
        }

        public async Task<bool> Register(Users request)
        {
            request.Id = Guid.NewGuid();
            _unitOfWork.GetRepository<Users>().Add(request);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<string> Authetate(LoginRequest request)
        {
            var user = await _unitOfWork.GetRepository<Users>().GetAll().Where(x => x.UserName == request.UserName && x.Password == request.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            //  var roles = await _unitOfWork.GetRepository<Roles>().FindAsync(x => x.RoleId == user.RoleId);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
               // new Claim(ClaimTypes.Role,roles.RoleName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Users Add(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<Users> AddAsync(Users entity)
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

        public void Delete(Users entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Users Find(Expression<Func<Users, bool>> match)
        {
            throw new NotImplementedException();
        }

        public ICollection<Users> FindAll(Expression<Func<Users, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Users>> FindAllAsync(Expression<Func<Users, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<Users> FindAsync(Expression<Func<Users, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Users Get(Guid id)
        {
            return _unitOfWork.GetRepository<Users>().GetById(id);
        }

        public ICollection<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Users>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<Users>().GetAll().ToListAsync();
        }

        public async Task<Users> GetAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Users>().GetAsyncById(id);
        }

        public Users Update(Users updated, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> UpdateAsync(Users updated, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TableResultJsonResponse<UserViewModel>> GetAllAsync(DataTableParameters parameters)
        {
            var data = new ConcurrentBag<UserViewModel>();
            var result = new TableResultJsonResponse<UserViewModel>();
            var users = _unitOfWork.GetRepository<Users>().GetAll();
            if (!string.IsNullOrEmpty(parameters.Search.Value))
            {
                users = _unitOfWork.GetRepository<Users>().GetAll().Where(x => x.UserName == parameters.Search.Value && x.Status == true);
            }
            else
            {
                users = _unitOfWork.GetRepository<Users>().GetAll().Where(x => x.Status == true);
            }
            var totalRecord = users.Count();
            var filteredUsers = await users.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            // var productsImgage = _unitOfWork.GetRepository<ProductImage>().GetAll();
            foreach (var user in filteredUsers)
            {
                var userInfo = _mapper.Map<UserViewModel>(user);
                //var productInfo = new ProductViewModel()
                //{
                //    ProductID = product.ProductID,
                //    ProductName = product.ProductName,
                //    //ImagePath = productsImgage.
                //};
                data.Add(userInfo);
            }
            result.Draw = parameters.Draw;
            result.RecordsTotal = totalRecord;
            result.RecordsFiltered = totalRecord;
            result.Data = data.ToList();

            return result;
        }

        public async Task<Users> AddAsync(UserRequest request)
        {
            var user = new Users()
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                DOB = DateTime.Now,
                PhoneNumber = request.PhoneNumber,
                Status = true,
                RoleId = request.RoleId

            };

            if (request.ThumbnailImage != null)
            {
                user.ImagePath = await this.SaveFile(request.ThumbnailImage);
            }

            _unitOfWork.GetRepository<Users>().Add(user);
            await _unitOfWork.CommitAsync();

            return user;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}