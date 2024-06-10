using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Services
{
    public class ImageUploadService
    {

        private readonly string _imagesDirectory;
        private readonly IWriteRepository<File> _fileWriteRepository;
        private readonly IWriteRepository<ImageUpload> _imageUploadWriteRepository;

        public ImageUploadService(string imagesDirectory, IWriteRepository<File> fileWriteRepository, IWriteRepository<ImageUpload> imageUploadWriteRepository)
        {
            _imagesDirectory = imagesDirectory;
            _fileWriteRepository = fileWriteRepository;
            _imageUploadWriteRepository = imageUploadWriteRepository;
        }

        public async Task UploadImageAsync(IFormFile file, Guid customerId)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(_imagesDirectory, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileEntity = new File
                {
                    Id = Guid.NewGuid(),
                    Name = fileName,
                    Path = filePath,
                    ExpirationDate = DateTime.Now.AddMonths(1),
                    ShouldStore = true,
                    ShouldDelete = false,
                    ImageUploadId = Guid.NewGuid()
                };

                var imageUpload = new ImageUpload
                {
                    Id = fileEntity.ImageUploadId,
                    CustomerId = customerId,
                    File = new List<File> { fileEntity }
                };

                await _imageUploadWriteRepository.AddAsync(imageUpload);
                await _fileWriteRepository.AddAsync(fileEntity);
            }
        }
    }

}
