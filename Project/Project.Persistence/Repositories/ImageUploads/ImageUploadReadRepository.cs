using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.ImageUploads;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.ImageUploads
{
    public class ImageUploadReadRepository : ReadRepository<ImageUpload, FinancialOCRDbContext>, IImageUploadReadRepository
    {
        public ImageUploadReadRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
