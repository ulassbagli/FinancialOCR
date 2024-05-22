using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.Companies.Commands.CreatedCommand;
using Project.Application.Features.Companies.Commands.DeletedCommand;
using Project.Application.Features.Companies.Commands.UpdateCommand;
using Project.Application.Features.Companies.Dtos;
using Project.Application.Features.Companies.Dtos.BaseDto;
using Project.Application.Features.Companies.Models;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Company, BaseCompanyDto>()
           //.ForMember(dto => dto.Name, opt => opt.MapFrom(d => d.User.FirstName))
           //.ForMember(dto => dto.Address, opt => opt.MapFrom(d => d.User.LastName))
           //.ForMember(dto => dto.TaxNo, opt => opt.MapFrom(d => d.User.Email))
           .ReverseMap();
            CreateMap<Company, CreateCompanyCommand>().ReverseMap();
            CreateMap<Company, UpdateCompanyCommand>().ReverseMap();
            CreateMap<Company, CompanyListDto>().ReverseMap();
            CreateMap<Company, DeletedCompanyDto>().ReverseMap();
            CreateMap<Company, DeleteCompanyCommand>().ReverseMap();
            CreateMap<IPaginate<Company>, CompanyListModel>().ReverseMap();
        }
    }
}
