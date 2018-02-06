using AutoMapper;
using Diabetes.Web.Models;
using Diabetes.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Logic
{
    public class DiabetesMappingProfiles : Profile
    {
        public DiabetesMappingProfiles()
        {
            CreateMap<CreateReadingViewModel, Reading>()
                .ForMember(m => m.Created, options => options.MapFrom(item => DateTimeOffset.UtcNow));
            CreateMap<ModifyReadingViewModel, Reading>();

            CreateMap<CreateInjectionViewModel, InsulinInjection>()
                .ForMember(m => m.Created, options => options.MapFrom(item => DateTime.UtcNow));
            CreateMap<ModifyInjectionViewModel, InsulinInjection>();

            CreateMap<CreateFoodViewModel, Food>()
                .ForMember(m => m.Created, options => options.MapFrom(item => DateTimeOffset.UtcNow));
            CreateMap<ModifyFoodViewModel, Food>();
        }
    }
}
