using AutoMapper;
using CS.EF.Models;
using CS.VM.ProductViewModel;
using CS.VM.Request;
using CS.VM.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWebApplication.AutoMapper
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        /// <param name="terminalSetting">The terminal setting.</param>
        public MappingProfile()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<RegisterRequest, Users>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Users, UserViewModel>();
            //CreateMap<ProductCategoryViewModels, ProductCategory>();
        }
    }
}