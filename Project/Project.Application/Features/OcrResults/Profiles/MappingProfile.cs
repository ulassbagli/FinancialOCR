using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Features.OcrResults.Commands.CreateCommand;
using Project.Application.Features.OcrResults.Commands.DeleteCommand;
using Project.Application.Features.OcrResults.Commands.UpdateCommand;
using Project.Application.Features.OcrResults.Dtos;
using Project.Application.Features.OcrResults.Dtos.BaseDto;
using Project.Application.Features.OcrResults.Models;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<OcrResult, BaseOcrResultDto>()
            //.ForMember(dto => dto.FirstName, opt => opt.MapFrom(d => d.User.FirstName))
            //.ForMember(dto => dto.LastName, opt => opt.MapFrom(d => d.User.LastName))
            //.ForMember(dto => dto.Email, opt => opt.MapFrom(d => d.User.Email))
            .ReverseMap();
            CreateMap<OcrResult, CreateOcrResultCommand>().ReverseMap();
            CreateMap<OcrResult, UpdateOcrResultCommand>().ReverseMap();
            CreateMap<OcrResult, DeletedOcrResultDto>().ReverseMap();
            CreateMap<OcrResult, DeleteOcrResultCommand>().ReverseMap();
            CreateMap<IPaginate<OcrResult>, OcrResultListModel>().ReverseMap();
        }
    }
}
