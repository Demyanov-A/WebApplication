using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Domain.Entities;
using WebApplication.Domain.ViewModels;

namespace WebApplication.Infrastructure.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(ViewModel => ViewModel.Name, opt => opt.MapFrom(Model => Model.FirstName))
                .ReverseMap();
        }
    }
}
