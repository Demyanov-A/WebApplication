using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Domain.ViewModels.Identity;

namespace WebApplication.Infrastructure.AutoMapper
{
    public class RegisterUserProfile : Profile
    {
        public RegisterUserProfile()
        {
            CreateMap<RegisterUserViewModel, User>()
                .ForMember(user => user.UserName, opt => opt.MapFrom(model => model.UserName));
        }
    }
}
