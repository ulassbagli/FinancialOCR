using Application.Features.Developers.Commands.CreateCommand;
using Application.Features.Developers.Commands.DeleteCommand;
using Application.Features.Developers.Commands.UpdateCommand;
using Application.Features.Developers.Dtos;
using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Developers.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Developer, BaseDeveloperDto>()
            .ForMember(dto=>dto.FirstName, opt=>opt.MapFrom(d=>d.User.FirstName))
            .ForMember(dto=>dto.LastName, opt=>opt.MapFrom(d=>d.User.LastName))
            .ForMember(dto=>dto.Email, opt=>opt.MapFrom(d=>d.User.Email))
            .ReverseMap();
        CreateMap<Developer, CreateDeveloperCommand>().ReverseMap();
        CreateMap<Developer, UpdateDeveloperCommand>().ReverseMap();
        CreateMap<Developer, DeveloperListDto>().ReverseMap();
        CreateMap<Developer, DeletedDeveloperDto>().ReverseMap();
        CreateMap<Developer, DeleteDeveloperCommand>().ReverseMap();
        CreateMap<IPaginate<Developer>, DeveloperListModel>().ReverseMap();
    }
}