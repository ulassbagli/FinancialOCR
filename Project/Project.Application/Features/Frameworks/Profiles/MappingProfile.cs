using Application.Features.Frameworks.Commands.CreateCommand;
using Application.Features.Frameworks.Commands.DeleteCommand;
using Application.Features.Frameworks.Commands.UpdateCommand;
using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Dtos.BaseDto;
using Application.Features.Frameworks.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Frameworks.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Framework, BaseFrameworkDto>().ForMember(b=>b.ProgrammingLanguageName, 
            opt=>opt.MapFrom(f=>f.ProgrammingLanguage.Name)).ReverseMap();
        CreateMap<Framework, CreateFrameworkCommand>().ReverseMap();
        CreateMap<Framework, FrameworkListDto>().ReverseMap();
        CreateMap<Framework, UpdateFrameworkCommand>().ReverseMap()
            .ForMember(dest => dest.ProgrammingLanguageId, opt => opt.Ignore()) 
            .AfterMap((src, dest) => dest.ProgrammingLanguageId = string.IsNullOrEmpty(src.ProgrammingLanguageId) ? dest.ProgrammingLanguageId : Guid.Parse(src.ProgrammingLanguageId))
            .ForAllMembers(opt=>opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Framework, DeletedFrameworkDto>().ReverseMap();
        CreateMap<Framework, DeleteFrameworkCommand>().ReverseMap();
        CreateMap<IPaginate<Framework>, FrameworkListModel>().ReverseMap();
    }
}