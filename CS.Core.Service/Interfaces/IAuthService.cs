using CS.VM.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CS.Core.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(LoginRequest request);
    }
}