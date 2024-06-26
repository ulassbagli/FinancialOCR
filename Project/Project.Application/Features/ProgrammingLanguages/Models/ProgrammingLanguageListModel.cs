﻿using Application.Features.ProgrammingLanguages.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.ProgrammingLanguages.Models;

public class ProgrammingLanguageListModel : BasePageableModel 
{
    public IEnumerable<ProgrammingLanguageListDto> Items { get; set; }
}