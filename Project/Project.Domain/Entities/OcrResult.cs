using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class OcrResult : Entity
    {
        public Guid Id { get; set; }
        public virtual Invoice Invoice { get; set; }
        public string resultText { get; set; }
        public float confidenceScore { get; set; }
    }
}
