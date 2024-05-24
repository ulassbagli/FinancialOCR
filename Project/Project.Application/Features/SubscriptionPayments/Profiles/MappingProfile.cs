using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.SubscriptionPayments.Command.CreateCommand;
using Project.Application.Features.SubscriptionPayments.Command.DeleteCommand;
using Project.Application.Features.SubscriptionPayments.Command.UpdateCommand;
using Project.Application.Features.SubscriptionPayments.Dtos;
using Project.Application.Features.SubscriptionPayments.Dtos.BaseDto;
using Project.Application.Features.SubscriptionPayments.Models;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SubscriptionPayment, BaseSubscriptionPaymentDto>()
                //.ForMember(dto => dto.FirstName, opt => opt.MapFrom(d => d.User.FirstName))
                //.ForMember(dto => dto.LastName, opt => opt.MapFrom(d => d.User.LastName))
                //.ForMember(dto => dto.Email, opt => opt.MapFrom(d => d.User.Email))
                .ReverseMap();
            CreateMap<SubscriptionPayment, CreateSubscriptionPaymentCommand>().ReverseMap();
            CreateMap<SubscriptionPayment, UpdateSubscriptionPaymentCommand>().ReverseMap();
            CreateMap<SubscriptionPayment, SubscriptionPaymentListDto>().ReverseMap();
            CreateMap<SubscriptionPayment, DeletedSubscriptionPaymentDto>().ReverseMap();
            CreateMap<SubscriptionPayment, DeleteSubscriptionPaymentCommand>().ReverseMap();
            CreateMap<IPaginate<SubscriptionPayment>, SubscriptionPaymentListModel>().ReverseMap();
        }
    }
}
