using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class File : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool ShouldStore { get; set; }
        public bool ShouldDelete { get; set; }
        public Guid ImageUploadId { get; set; }
        public ImageUpload ImageUpload { get; set; }

    }
}
