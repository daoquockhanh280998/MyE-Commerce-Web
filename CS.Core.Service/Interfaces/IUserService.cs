using AdminApp.Core.UoW;
using CS.EF.Models;
using CS.VM.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CS.Core.Service.Interfaces
{
    public interface IUserService : IService<Users, IRepository<Users>>
    {
        Task<string> Authetate(LoginRequest request);

        Task<bool> Register(Users request);
    }
}