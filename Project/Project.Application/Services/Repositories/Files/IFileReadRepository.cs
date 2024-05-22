using Core.Persistence.Repositories;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Services.Repositories.Files
{
    public interface IFileReadRepository : IReadRepository<File>
    {
    }
}
