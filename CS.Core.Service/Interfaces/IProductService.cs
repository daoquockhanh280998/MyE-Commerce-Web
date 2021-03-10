using AdminApp.Core.UoW;
using CS.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Core.Service.Interfaces
{
    public interface IProductService : IService<Product, IRepository<Product>>
    {
    }
}