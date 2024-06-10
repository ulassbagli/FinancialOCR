using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly string _imagesDirectory;
        private readonly ImageUploadService _imageUploadService;
        private readonly IReadRepository<ImageUpload> _imageUploadReadRepository;

        public ImageUploadController(ImageUploadService imageUploadService, IReadRepository<ImageUpload> imageUploadReadRepository)
        {
            _imageUploadService = imageUploadService;
            _imageUploadReadRepository = imageUploadReadRepository;

            _imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(_imagesDirectory))
            {
                Directory.CreateDirectory(_imagesDirectory);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([Bind] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_imagesDirectory, fileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterImages([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var uploads = await _imageUploadReadRepository.GetByFilterAsync(u => u.File.Any(f => f.ExpirationDate >= startDate && f.ExpirationDate <= endDate));
            return Ok(uploads);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageUploadById(string id)
        {
            var upload = await _imageUploadReadRepository.GetByIdAsync(id);
            if (upload == null) return NotFound();
            return Ok(upload);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetImageUploadsByCustomerId(Guid customerId)
        {
            var uploads = await _imageUploadReadRepository.GetByFilterAsync(u => u.CustomerId == customerId);
            return Ok(uploads);
        }
    }
}