using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Repositories.ImageUploads
{
    public interface IImageUploadWriteRepository : IWriteRepository<ImageUpload>
    {
    }
        
}
