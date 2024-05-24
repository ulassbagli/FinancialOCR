using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.Invoices.Commands.CreateCommand;
using Project.Application.Features.Invoices.Commands.DeleteCommand;
using Project.Application.Features.Invoices.Commands.UpdateCommand;
using Project.Application.Features.Invoices.Dtos;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using Project.Application.Features.Invoices.Models;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Invoice, BaseInvoiceDto>()
                //.ForMember(dto => dto.FirstName, opt => opt.MapFrom(d => d.User.FirstName))
                //.ForMember(dto => dto.LastName, opt => opt.MapFrom(d => d.User.LastName))
                //.ForMember(dto => dto.Email, opt => opt.MapFrom(d => d.User.Email))
                .ReverseMap();
            CreateMap<Invoice, CreateInvoiceCommand>().ReverseMap();
            CreateMap<Invoice, UpdateInvoiceCommand>().ReverseMap();
            CreateMap<Invoice, InvoiceListDto>().ReverseMap();
            CreateMap<Invoice, DeletedInvoiceDto>().ReverseMap();
            CreateMap<Invoice, DeleteInvoiceCommand>().ReverseMap();
            CreateMap<IPaginate<Invoice>, InvoiceListModel>().ReverseMap();
        }
    }
}
