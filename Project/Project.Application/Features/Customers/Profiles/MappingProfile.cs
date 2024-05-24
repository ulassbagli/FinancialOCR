using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.Customers.Commands.CreateCommand;
using Project.Application.Features.Customers.Commands.DeleteCommand;
using Project.Application.Features.Customers.Commands.UpdateCommand;
using Project.Application.Features.Customers.Dtos;
using Project.Application.Features.Customers.Dtos.BaseDto;
using Project.Application.Features.Customers.Models;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, BaseCustomerDto>()
                //.ForMember(dto => dto.FirstName, opt => opt.MapFrom(d => d.User.FirstName))
                //.ForMember(dto => dto.LastName, opt => opt.MapFrom(d => d.User.LastName))
                //.ForMember(dto => dto.Email, opt => opt.MapFrom(d => d.User.Email))
                .ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
            CreateMap<Customer, CustomerListDto>().ReverseMap();
            CreateMap<Customer, DeletedCustomerDto>().ReverseMap();
            CreateMap<Customer, DeleteCustomerCommand>().ReverseMap();
            CreateMap<IPaginate<Customer>, CustomerListModel>().ReverseMap();
        }
    }
}
