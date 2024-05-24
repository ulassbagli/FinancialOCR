using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Dtos.BaseDto
{
    public class BaseOcrResultDto
    {
        public Guid Id { get; set; }
        public string resultText { get; set; }
        public float confidenceScore { get; set; }
    }
}
