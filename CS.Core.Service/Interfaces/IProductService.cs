using AdminApp.Core.UoW;
using AdminApp.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Core.Service.Interfaces
{
    public interface IProductService : IService<Product, IRepository<Product>>
    {
    }
}
