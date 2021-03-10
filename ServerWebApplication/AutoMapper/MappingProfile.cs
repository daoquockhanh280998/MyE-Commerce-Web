using AutoMapper;
using CS.EF.Models;
using CS.VM.Request;
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
            //CreateMap<ProductCategoryViewModels, ProductCategory>();
        }
    }
}