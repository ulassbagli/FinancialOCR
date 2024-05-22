﻿using Core.Persistence.Repositories;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Repositories.OcrResults
{
    public interface IOcrResultWriteRepository : IWriteRepository<OcrResult>
    {
    }
}
