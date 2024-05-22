using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.Accountants.Dtos.BaseDto;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Accountants, BaseAccountantDto>();
        }
    }
}
