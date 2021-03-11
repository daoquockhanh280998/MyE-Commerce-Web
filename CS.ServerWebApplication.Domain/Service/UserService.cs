using AdminApp.Core.UoW;
using AutoMapper;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CS.Server.Domain.Service
{
    public class UserService : IUserService
    {
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
            IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
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
            throw new NotImplementedException();
        }

        public ICollection<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Users>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Users Update(Users updated, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> UpdateAsync(Users updated, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}